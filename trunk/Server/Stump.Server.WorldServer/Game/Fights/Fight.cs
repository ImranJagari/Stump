﻿using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using Stump.Core.Attributes;
using Stump.Core.Extensions;
using Stump.Core.Pool;
using Stump.Core.Timers;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Game.Fights.Results;
using Stump.Server.WorldServer.Game.Fights.Triggers;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Maps.Pathfinding;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Handlers.Actions;
using Stump.Server.WorldServer.Handlers.Basic;
using Stump.Server.WorldServer.Handlers.Characters;
using Stump.Server.WorldServer.Handlers.Context;
using TriggerType = Stump.Server.WorldServer.Game.Fights.Triggers.TriggerType;

namespace Stump.Server.WorldServer.Game.Fights
{
    public abstract class Fight : ICharacterContainer
    {
        protected readonly Logger logger = LogManager.GetCurrentClassLogger();

        #region Config

        [Variable]
        public static int PlacementPhaseTime = 30000;

        /// <summary>
        ///   Delay for player's turn
        /// </summary>
        [Variable]
        public static int TurnTime = 35000;

        /// <summary>
        ///   Delay before force turn to end
        /// </summary>
        [Variable]
        public static int TurnEndTimeOut = 5000;

        /// <summary>
        ///   Delay before force turn to end
        /// </summary>
        [Variable]
        public static int EndFightTimeOut = 10000;

        #endregion

        #region Events

        #endregion

        #region Constructor

        protected Fight(int id, Map fightMap, FightTeam blueTeam, FightTeam redTeam)
        {
            Id = id;
            Map = fightMap;
            BlueTeam = blueTeam;
            BlueTeam.Fight = this;
            RedTeam = redTeam;
            RedTeam.Fight = this;
            m_teams = new[] {RedTeam, BlueTeam};

            TimeLine = new TimeLine(this);
            Leavers = new List<FightActor>();

            BlueTeam.FighterAdded += OnFighterAdded;
            BlueTeam.FighterRemoved += OnFighterRemoved;
            RedTeam.FighterAdded += OnFighterAdded;
            RedTeam.FighterRemoved += OnFighterRemoved;

            CreationTime = DateTime.Now;
        }

        #endregion

        #region Properties

        private readonly ReversedUniqueIdProvider m_contextualIdProvider = new ReversedUniqueIdProvider(0);
        private readonly UniqueIdProvider m_triggerIdProvider = new UniqueIdProvider();
        private TimerEntry m_endFightTimer;
        private bool m_isInitialized;
        private bool m_disposed;
        private TimerEntry m_placementTimer;
        private TimerEntry m_turnTimer;
        protected FightTeam[] m_teams;

        public int Id
        {
            get;
            private set;
        }

        public Map Map
        {
            get;
            private set;
        }

        public abstract FightTypeEnum FightType
        {
            get;
        }

        public FightState State
        {
            get;
            private set;
        }

        public bool IsStarted
        {
            get;
            private set;
        }

        public DateTime CreationTime
        {
            get;
            private set;
        }

        public DateTime StartTime
        {
            get;
            private set;
        }

        public short AgeBonus
        {
            get;
            protected set;
        }

        public FightTeam RedTeam
        {
            get;
            private set;
        }

        public FightTeam BlueTeam
        {
            get;
            private set;
        }

        public TimeLine TimeLine
        {
            get;
            private set;
        }

        public FightActor FighterPlaying
        {
            get { return TimeLine.Current; }
        }

        public ReadyChecker ReadyChecker
        {
            get;
            protected set;
        }

        internal List<FightActor> Fighters
        {
            get { return TimeLine.Fighters; }
        }

        internal List<FightActor> Leavers
        {
            get;
            private set;
        }

        public bool BladesVisible
        {
            get;
            private set;
        }

        #endregion

        #region Phases

        protected void SetFightState(FightState state)
        {
            State = state;

            UnBindFightersEvents();
            BindFightersEvents();

            OnStateChanged();
        }

        protected virtual void OnStateChanged()
        {
            if (State != FightState.Placement && BladesVisible)
                HideBlades();
        }

        public void Initialize()
        {
            if (m_isInitialized)
                return;

            ProcessInitialization();

            m_isInitialized = true;
        }

        protected virtual void ProcessInitialization()
        {
        }

        public virtual void StartFighting()
        {
            if (State != FightState.Placement &&
                State != FightState.NotStarted) // we can imagine a fight without placement phase
                return;

            SetFightState(FightState.Fighting);
            StartTime = DateTime.Now;
            IsStarted = true;

            HideBlades();

            TimeLine.OrderLine();

            ContextHandler.SendGameEntitiesDispositionMessage(Clients, GetAllFighters());
            ContextHandler.SendGameFightStartMessage(Clients);
            ContextHandler.SendGameFightTurnListMessage(Clients, this);
            ContextHandler.SendGameFightSynchronizeMessage(Clients, this);

            StartTurn();
        }

        #region EndFight

        public bool CheckFightEnd()
        {
            if (RedTeam.AreAllDead() || BlueTeam.AreAllDead())
            {
                EndFight();
                return true;
            }

            return false;
        }

        public void CancelFight()
        {
            if (!CanCancelFight())
                return;

            if (State != FightState.Placement)
            {
                EndFight();
                return;
            }

            SetFightState(FightState.Ended);

            ForEach(character =>
                        {
                            ContextHandler.SendGameFightEndMessage(character.Client, this);
                            character.RejoinMap();
                        });

            Dispose();
        }

        public void EndFight()
        {
            if (State == FightState.Placement)
                CancelFight();

            if (State == FightState.Ended)
                return;

            SetFightState(FightState.Ended);

            if (m_turnTimer != null)
                m_turnTimer.Stop();

            if (ReadyChecker != null)
            {
                ReadyChecker.Cancel();
            }

            ReadyChecker = ReadyChecker.RequestCheck(this, () => OnFightEnded(), actors => OnFightEnded());
        }

        protected virtual void OnFightEnded()
        {
            ReadyChecker = null;
            ResetFightersProperties();

            List<IFightResult> results = GenerateResults().ToList();

            ApplyResults(results);

            ContextHandler.SendGameFightEndMessage(Clients, this, results.Select(entry => entry.GetFightResultListEntry()));

            ForEach(entry => entry.RejoinMap());
            Dispose();
        }

        protected virtual void ResetFightersProperties()
        {
            foreach (FightActor fighter in Fighters)
            {
                fighter.ResetUsedPoints();
                fighter.RemoveAndDispellAllBuffs();

                foreach (var field in fighter.Stats.Fields)
                {
                    if (field.Key != CaracteristicsEnum.Health)
                        field.Value.Context = 0;
                }
            }
        }

        protected abstract IEnumerable<IFightResult> GenerateResults();

        protected void ApplyResults(IEnumerable<IFightResult> results)
        {
            foreach (IFightResult fightResult in results)
            {
                fightResult.Apply();
            }
        }

        protected void Dispose()
        {
            if (m_disposed)
                return;

            m_disposed = true;

            OnDisposed();

            UnBindFightersEvents();

            Map.RemoveFight(this);
            FightManager.Instance.Remove(this);
            GC.SuppressFinalize(this);
        }

        protected virtual void OnDisposed()
        {
            if (ReadyChecker != null)
                ReadyChecker.Cancel();

            if (m_placementTimer != null)
                m_placementTimer.Stop();

            if (m_turnTimer != null)
                m_turnTimer.Stop();
        }

        #endregion

        #region Placement

        public virtual void StartPlacement()
        {
            if (State != FightState.NotStarted)
                return;

            SetFightState(FightState.Placement);

            RandomnizePositions(RedTeam);
            RandomnizePositions(BlueTeam);

            ShowBlades();
            Map.AddFight(this);
        }

        #region Blades

        private void FindBladesPlacement()
        {
            if (RedTeam.Leader.MapPosition.Cell.Id != BlueTeam.Leader.MapPosition.Cell.Id)
            {
                RedTeam.BladePosition = RedTeam.Leader.MapPosition.Clone();
                BlueTeam.BladePosition = BlueTeam.Leader.MapPosition.Clone();
            }
            else
            {
                Cell cell = Map.GetRandomAdjacentFreeCell(RedTeam.Leader.MapPosition.Point);

                // if cell not found we superpose both blades
                if (cell.Equals(Cell.Null))
                {
                    RedTeam.BladePosition = RedTeam.Leader.MapPosition.Clone();
                }
                else // else we take an adjacent cell
                {
                    ObjectPosition pos = RedTeam.Leader.MapPosition.Clone();
                    pos.Cell = cell;
                    RedTeam.BladePosition = pos;
                }

                BlueTeam.BladePosition = BlueTeam.Leader.MapPosition.Clone();
            }
        }

        public void ShowBlades()
        {
            if (BladesVisible || State != FightState.Placement)
                return;

            if (RedTeam.BladePosition == null ||
                BlueTeam.BladePosition == null)
                FindBladesPlacement();

            ContextHandler.SendGameRolePlayShowChallengeMessage(Map.Clients, this);

            RedTeam.TeamOptionsChanged += OnTeamOptionsChanged;
            BlueTeam.TeamOptionsChanged += OnTeamOptionsChanged;

            BladesVisible = true;
        }

        public void HideBlades()
        {
            if (!BladesVisible)
                return;

            ContextHandler.SendGameRolePlayRemoveChallengeMessage(Map.Clients, this);

            RedTeam.TeamOptionsChanged -= OnTeamOptionsChanged;
            BlueTeam.TeamOptionsChanged -= OnTeamOptionsChanged;

            BladesVisible = false;
        }

        public void UpdateBlades(FightTeam team)
        {
            if (!BladesVisible)
                return;

            ContextHandler.SendGameFightUpdateTeamMessage(Map.Clients, this, team);
        }

        private void OnTeamOptionsChanged(FightTeam team, FightOptionsEnum option)
        {
            ContextHandler.SendGameFightOptionStateUpdateMessage(Clients, team, option, team.GetOptionState(option));
            ContextHandler.SendGameFightOptionStateUpdateMessage(Map.Clients, team, option, team.GetOptionState(option));
        }

        #endregion

        #region Placement methods

        public bool FindRandomFreeCell(FightActor fighter, out Cell cell, bool placement = true)
        {
            Cell[] availableCells = fighter.Team.PlacementCells.Where(entry => GetOneFighter(entry) == null || GetOneFighter(entry) == fighter).ToArray();

            var random = new Random();

            if (availableCells.Length == 0 && placement)
            {
                cell = Cell.Null;
                return false;
            }

            // if not in placement phase, get a random free cell on the map
            if (availableCells.Length == 0 && !placement)
            {
                List<int> cells = Enumerable.Range(0, (int) MapPoint.MapSize).ToList();
                foreach (FightActor actor in GetAllFighters(actor => cells.Contains(actor.Cell.Id)))
                {
                    cells.Remove(actor.Cell.Id);
                }

                cell = Map.Cells[cells[random.Next(cells.Count)]];

                return true;
            }

            cell = availableCells[random.Next(availableCells.Length)];

            return true;
        }


        public bool RandomnizePosition(FightActor fighter)
        {
            if (State != FightState.Placement)
                throw new Exception("State != Placement, cannot random placement position");

            Cell cell;
            if (!FindRandomFreeCell(fighter, out cell))
            {
                fighter.LeaveFight(); // no place more than we kick the actor to avoid bugs
                return false;
            }

            fighter.ChangePrePlacement(cell);
            return true;
        }

        public void RandomnizePositions(FightTeam team)
        {
            if (State != FightState.Placement)
                throw new Exception("State != Placement, cannot random placement position");

            IEnumerable<Cell> shuffledCells = team.PlacementCells.Shuffle();
            IEnumerator<Cell> enumerator = shuffledCells.GetEnumerator();
            foreach (FightActor fighter in team.GetAllFighters())
            {
                enumerator.MoveNext();

                fighter.ChangePrePlacement(enumerator.Current);
            }
            enumerator.Dispose();
        }

        public DirectionsEnum FindPlacementDirection(FightActor fighter)
        {
            if (State != FightState.Placement)
                throw new Exception("State != Placement, cannot give placement direction");

            FightTeam team = fighter.Team == RedTeam ? BlueTeam : RedTeam;

            Tuple<Cell, uint> closerCell = null;
            foreach (Cell cell in team.PlacementCells)
            {
                var point = new MapPoint(cell);

                if (closerCell == null)
                    closerCell = Tuple.Create(cell,
                                              fighter.Position.Point.DistanceToCell(point));
                else
                {
                    if (fighter.Position.Point.DistanceToCell(point) < closerCell.Item2)
                        closerCell = Tuple.Create(cell,
                                                  fighter.Position.Point.DistanceToCell(point));
                }
            }

            if (closerCell == null)
                return fighter.Position.Direction;

            return fighter.Position.Point.OrientationTo(new MapPoint(closerCell.Item1), false);
        }

        public bool KickFighter(FightActor fighter)
        {
            if (!Fighters.Contains(fighter))
                return false;

            if (State != FightState.Placement)
                return false;

            fighter.Team.RemoveFighter(fighter);

            if (fighter is CharacterFighter)
            {
                ( (CharacterFighter)fighter ).Character.RejoinMap();
            }

            CheckFightEnd();

            return true;
        }

        /// <summary>
        ///   Set the ready state of a character
        /// </summary>
        protected virtual void OnSetReady(FightActor fighter, bool isReady)
        {
            if (State != FightState.Placement)
                return;

            ContextHandler.SendGameFightHumanReadyStateMessage(Clients, fighter);

            if (RedTeam.AreAllReady() && BlueTeam.AreAllReady())
                StartFighting();
        }


        /// <summary>
        ///   Check if a character can change position (before the fight is started).
        /// </summary>
        /// <param name = "fighter"></param>
        /// <param name="cell"></param>
        /// <returns>If change is possible</returns>
        public virtual bool CanChangePosition(FightActor fighter, Cell cell)
        {
            FightActor figtherOnCell = GetOneFighter(cell);

            return State == FightState.Placement &&
                   fighter.Team.PlacementCells.Contains(cell) &&
                   (figtherOnCell == fighter || figtherOnCell == null);
        }

        protected virtual void OnChangePreplacementPosition(FightActor fighter, ObjectPosition objectPosition)
        {
            UpdateFightersPlacementDirection();

            ContextHandler.SendGameEntitiesDispositionMessage(Clients, GetAllFighters());
        }

        protected void UpdateFightersPlacementDirection()
        {
            foreach (FightActor fighter in Fighters)
            {
                fighter.Position.Direction = FindPlacementDirection(fighter);
            }
        }

        #endregion

        #region Kick



        #endregion

        #endregion

        #endregion

        #region Add/Remove Fighter

        protected virtual void OnFighterAdded(FightTeam team, FightActor actor)
        {
            // todo : summons
            if (State == FightState.Fighting ||
                State == FightState.Ended)
            {
                throw new Exception("Cannot use fight actor while fighting");
            }

            TimeLine.Fighters.Add(actor);
            BindFighterEvents(actor);

            if (State == FightState.Placement)
                if (!RandomnizePosition(actor))
                    return;

            if (actor is CharacterFighter)
                OnCharacterAdded(actor as CharacterFighter);

            ContextHandler.SendGameFightShowFighterMessage(Clients, actor);

            // update blades if shown
            if (BladesVisible)
                UpdateBlades(team);
        }

        protected virtual void OnCharacterAdded(CharacterFighter fighter)
        {
            Character character = fighter.Character;

            Clients.Add(character.Client);

            SendGameFightJoinMessage(fighter);

            if (State == FightState.Placement || State == FightState.NotStarted)
            {
                ContextHandler.SendGameFightPlacementPossiblePositionsMessage(character.Client, this, fighter.Team.Id);

                foreach (FightActor fightMember in GetAllFighters())
                    ContextHandler.SendGameFightShowFighterMessage(character.Client, fightMember);

                ContextHandler.SendGameEntitiesDispositionMessage(character.Client, GetAllFighters());

                ContextHandler.SendGameFightUpdateTeamMessage(character.Client, this, RedTeam);
                ContextHandler.SendGameFightUpdateTeamMessage(character.Client, this, BlueTeam);
            }
        }


        protected virtual void OnFighterRemoved(FightTeam team, FightActor actor)
        {
            TimeLine.RemoveFighter(actor);
            UnBindFighterEvents(actor);

            if (actor is CharacterFighter)
                OnCharacterRemoved(actor as CharacterFighter);

            if (State == FightState.Placement)
            {
                ContextHandler.SendGameFightRemoveTeamMemberMessage(Clients, actor);
            }
            else if (State == FightState.Fighting)
            {
                ContextHandler.SendGameContextRemoveElementMessage(Clients, actor);
            }

            if (BladesVisible)
                UpdateBlades(team);
        }

        protected virtual void OnCharacterRemoved(CharacterFighter fighter)
        {
            Clients.Remove(fighter.Character.Client);
        }

        #endregion

        #region Turn Management

        public void StartTurn()
        {
            if (!CheckFightEnd())
            {
                OnTurnStarted();
            }
        }

        public event Action<Fight, FightActor> TurnStarted;

        protected virtual void OnTurnStarted()
        {
            DecrementGlyphDuration(FighterPlaying);
            FighterPlaying.DecrementAllCastedBuffsDuration();
            FighterPlaying.TriggerBuffs(Buffs.TriggerType.TURN_BEGIN);
            TriggerMarks(FighterPlaying.Cell, FighterPlaying, TriggerType.TURN_BEGIN);

            ContextHandler.SendGameFightSynchronizeMessage(Clients, this);
            ForEach(entry => CharacterHandler.SendCharacterStatsListMessage(entry.Client));

            if (TimeLine.Index == 0)
                ContextHandler.SendGameFightNewRoundMessage(Clients, TimeLine.RoundNumber);

            ContextHandler.SendGameFightTurnStartMessage(Clients, FighterPlaying.Id,
                                                         TurnTime);

            m_turnTimer = Map.Area.CallDelayed(TurnTime, StopTurn);

            Action<Fight, FightActor> evnt = TurnStarted;
            if (evnt != null)
                evnt(this, FighterPlaying);
        }

        public void StopTurn()
        {
            if (m_turnTimer != null)
                m_turnTimer.Stop();

            if (ReadyChecker != null)
            {
                logger.Error("Last ReadyChecker was not disposed.");
                ReadyChecker.Cancel();
                ReadyChecker = null;
            }

            if (CheckFightEnd())
                return;

            OnTurnStopped();

            ReadyChecker = ReadyChecker.RequestCheck(this, PassTurn, LagAndPassTurn);
        }

        protected virtual void OnTurnStopped()
        {
            FighterPlaying.TriggerBuffs(Buffs.TriggerType.TURN_END);
            FighterPlaying.ResetUsedPoints();

            if (IsSequencing)
                EndSequence(Sequence, true);

            if (WaitAcknowledgment)
                AcknowledgeAction();

            ContextHandler.SendGameFightTurnEndMessage(Clients, FighterPlaying);
        }

        protected void LagAndPassTurn(NamedFighter[] laggers)
        {
            // some guys are lagging !
            OnLaggersSpotted(laggers);

            PassTurn();
        }

        protected void PassTurn()
        {
            ReadyChecker = null;

            if (CheckFightEnd())
                return;

            if (!TimeLine.SelectNextFighter())
            {
                if (!CheckFightEnd())
                {
                    logger.Error("Someting goes wrong : no more actors area available to play but the fight is not ended");
                }

                return;
            }

            OnTurnPassed();

            StartTurn();
        }

        protected virtual void OnTurnPassed()
        {
            if (IsSequencing)
                EndSequence(Sequence, true);

            if (WaitAcknowledgment)
                AcknowledgeAction();
        }

        #endregion

        #region Events Binders

        private void UnBindFightersEvents()
        {
            foreach (FightActor fighter in Fighters)
            {
                UnBindFighterEvents(fighter);
            }
        }

        private void UnBindFighterEvents(FightActor actor)
        {
            actor.ReadyStateChanged -= OnSetReady;
            actor.PrePlacementChanged -= OnChangePreplacementPosition;
            actor.FighterLeft -= OnPlayerLeft;

            actor.StartMoving -= OnStartMoving;
            actor.StopMoving -= OnStopMoving;
            actor.PositionChanged -= OnPositionChanged;
            actor.FightPointsVariation -= OnFightPointsVariation;
            actor.LifePointsChanged -= OnLifePointsChanged;
            actor.DamageReducted -= OnDamageReducted;
            actor.SpellCasting -= OnSpellCasting;
            actor.SpellCasted -= OnSpellCasted;
            actor.BuffAdded -= OnBuffAdded;
            actor.BuffRemoved -= OnBuffRemoved;
            actor.Dead -= OnDead;


            var fighter = actor as CharacterFighter;

            if (fighter != null)
            {
                fighter.Character.LoggedOut -= OnPlayerLoggout;
            }
        }

        private void BindFightersEvents()
        {
            foreach (FightActor fighter in Fighters)
            {
                BindFighterEvents(fighter);
            }
        }

        private void BindFighterEvents(FightActor actor)
        {
            if (State == FightState.Placement)
            {
                actor.FighterLeft += OnPlayerLeft;
                actor.ReadyStateChanged += OnSetReady;
                actor.PrePlacementChanged += OnChangePreplacementPosition;
            }

            if (State == FightState.Fighting)
            {
                actor.FighterLeft += OnPlayerLeft;
                actor.StartMoving += OnStartMoving;
                actor.StopMoving += OnStopMoving;
                actor.PositionChanged += OnPositionChanged;
                actor.FightPointsVariation += OnFightPointsVariation;
                actor.LifePointsChanged += OnLifePointsChanged;
                actor.DamageReducted += OnDamageReducted;

                actor.SpellCasting += OnSpellCasting;
                actor.SpellCasted += OnSpellCasted;

                actor.BuffAdded += OnBuffAdded;
                actor.BuffRemoved += OnBuffRemoved;

                actor.Dead += OnDead;
            }

            var fighter = actor as CharacterFighter;

            if (fighter != null)
            {
                fighter.Character.LoggedOut += OnPlayerLoggout;
            }
        }

        #endregion

        #region Turn Actions

        #region Death

        protected virtual void OnDead(FightActor fighter, FightActor killedBy)
        {
            StartSequence(SequenceTypeEnum.SEQUENCE_CHARACTER_DEATH);

            ActionsHandler.SendGameActionFightDeathMessage(Clients, fighter);

            EndSequence(SequenceTypeEnum.SEQUENCE_CHARACTER_DEATH);
        }

        #endregion

        #region Movement

        protected virtual void OnStartMoving(ContextActor actor, Path path)
        {
            var fighter = actor as FightActor;

            if (!fighter.IsFighterTurn())
                return;

            if (path.IsEmpty() || path.MPCost == 0)
                return;

            IEnumerable<short> movementsKeys = path.GetServerPathKeys();
            Cell[] cells = path.GetPath();

            for (int i = 1; i < cells.Length; i++)
            {
                // if there is a trap on the way we trigger it
                if (m_triggers.Any(entry => entry.TriggerType == TriggerType.MOVE && entry.ContainsCell(cells[i])))
                    path.CutPath(i);
            }

            StartSequence(SequenceTypeEnum.SEQUENCE_MOVE);
            ContextHandler.SendGameMapMovementMessage(Clients, movementsKeys, fighter);
            actor.StopMove();
            EndSequence(SequenceTypeEnum.SEQUENCE_MOVE);
        }

        protected virtual void OnStopMoving(ContextActor actor, Path path, bool canceled)
        {
            var fighter = actor as FightActor;

            if (!fighter.IsFighterTurn())
                return;

            if (canceled)
                return; // error, mouvement shouldn't be canceled in a fight.

            fighter.UseMP((short) path.MPCost);
            fighter.TriggerBuffs(Buffs.TriggerType.MOVE);
        }

        protected virtual void OnPositionChanged(ContextActor actor, ObjectPosition objectPosition)
        {
            var fighter = actor as FightActor;

            TriggerMarks(fighter.Cell, fighter, TriggerType.MOVE);
        }

        #endregion

        #region Health & Actions points

        protected virtual void OnLifePointsChanged(FightActor actor, int delta, FightActor from)
        {
            // todo : not managed
            short permanentDamages = 0;
            var loss = (short) (-delta);

            ActionsHandler.SendGameActionFightLifePointsLostMessage(Clients, from ?? actor, actor, loss, permanentDamages);
        }

        protected virtual void OnFightPointsVariation(FightActor actor, ActionsEnum action, FightActor source, FightActor target, short delta)
        {
            ActionsHandler.SendGameActionFightPointsVariationMessage(Clients, action, source, target, delta);
        }

        protected virtual void OnDamageReducted(FightActor fighter, FightActor source, int reduction)
        {
            ActionsHandler.SendGameActionFightReduceDamagesMessage(Clients, source, fighter, reduction);
        }

        #endregion

        #region Spells

        protected virtual void OnSpellCasting(FightActor caster, Spell spell, Cell target, FightSpellCastCriticalEnum critical, bool silentCast)
        {
            ContextHandler.SendGameActionFightSpellCastMessage(Clients, ActionsEnum.ACTION_FIGHT_CAST_SPELL,
                                                               caster, target, critical, silentCast, spell);

            if (critical == FightSpellCastCriticalEnum.CRITICAL_FAIL)
                EndSequence(SequenceTypeEnum.SEQUENCE_SPELL);
        }

        protected virtual void OnSpellCasted(FightActor caster, Spell spell, Cell target, FightSpellCastCriticalEnum critical, bool silentCast)
        {
            EndSequence(SequenceTypeEnum.SEQUENCE_SPELL);

            CheckFightEnd();
        }

        #endregion

        #region Buffs

        protected virtual void OnBuffRemoved(FightActor target, Buff buff)
        {
        }

        protected virtual void OnBuffAdded(FightActor target, Buff buff)
        {
            ContextHandler.SendGameActionFightDispellableEffectMessage(Clients, buff);
        }

        #endregion

        #region Sequences

        private SequenceTypeEnum m_lastSequenceAction;
        private int m_sequenceLevel;

        public SequenceTypeEnum Sequence
        {
            get;
            private set;
        }

        public bool IsSequencing
        {
            get;
            private set;
        }

        public bool WaitAcknowledgment
        {
            get;
            private set;
        }

        public bool StartSequence(SequenceTypeEnum sequenceType)
        {
            // even if a sequence is already running we just increase the level variable and notify the last action
            m_lastSequenceAction = sequenceType;
            m_sequenceLevel++;

            if (IsSequencing)
                return false;

            IsSequencing = true;
            Sequence = sequenceType;

            ActionsHandler.SendSequenceStartMessage(Clients, TimeLine.Current, sequenceType);

            return true;
        }


        public bool EndSequence(SequenceTypeEnum sequenceType, bool force = false)
        {
            if (!IsSequencing)
                return false;

            m_sequenceLevel--;

            if (m_sequenceLevel > 0 && !force)
                return false;

            IsSequencing = false;
            WaitAcknowledgment = true;

            ActionsHandler.SendSequenceEndMessage(Clients, TimeLine.Current, sequenceType, m_lastSequenceAction);

            return true;
        }

        public virtual void AcknowledgeAction()
        {
            WaitAcknowledgment = false;

            // todo : find the right usage
        }

        #endregion

        #endregion

        #region Non Turn Actions

        protected virtual void OnPlayerLeft(FightActor fighter)
        {
            if (State == FightState.Placement)
            {
                // we don't send the packet that say that the actor is dead
                fighter.Stats.Health.DamageTaken = (short) (fighter.Stats.Health.TotalMax - 1);

                if (CheckFightEnd())
                    return;

                fighter.Team.RemoveFighter(fighter);

                if (fighter is CharacterFighter)
                {
                    Character character = ((CharacterFighter) fighter).Character;

                    ContextHandler.SendGameFightEndMessage(character.Client, this, Fighters.Select(entry => entry.GetFightResult().GetFightResultListEntry()));

                    character.RejoinMap();
                }
            }
            else
            {
                fighter.Die();

                if (fighter is CharacterFighter)
                {
                    Character character = ( (CharacterFighter)fighter ).Character;

                    // wait the character to be ready
                    var readyChecker = new ReadyChecker(this, new[] { ( (CharacterFighter)fighter ) });
                    Action<ReadyChecker> sendResults =
                        (obj) =>
                        {
                            ( (CharacterFighter)fighter ).PersonalReadyChecker = null;
                            bool isfighterTurn = fighter.IsFighterTurn();

                            ContextHandler.SendGameFightLeaveMessage(Clients, fighter);

                            fighter.Team.RemoveFighter(fighter);
                            Leavers.Add(fighter);

                            ContextHandler.SendGameFightEndMessage(character.Client, this, GetAllFightersAndLeavers().Select(entry => entry.GetFightResult().GetFightResultListEntry()));

                            character.RejoinMap();

                            if (CheckFightEnd())
                                return;

                            if (isfighterTurn)
                                StopTurn();
                        };
                    readyChecker.Success += sendResults;
                    readyChecker.Timeout += (obj, laggers) => sendResults(obj);

                    ( (CharacterFighter)fighter ).PersonalReadyChecker = readyChecker;
                    readyChecker.Start();

                    Clients.Remove(character.Client);
                }
                else
                {
                    ContextHandler.SendGameFightLeaveMessage(Clients, fighter);
                }
            }
        }

        protected virtual void OnPlayerLoggout(Character character)
        {
            if (!character.IsFighting() || character.Fight != this)
                return;

            character.Fighter.LeaveFight();
        }

        #endregion

        #region Triggers

        private readonly List<MarkTrigger> m_triggers = new List<MarkTrigger>();

        public void AddTriger(MarkTrigger trigger)
        {
            trigger.Triggered += OnMarkTriggered;
            m_triggers.Add(trigger);

            foreach (CharacterFighter fighter in GetAllFighters<CharacterFighter>())
            {
                ContextHandler.SendGameActionFightMarkCellsMessage(fighter.Character.Client, trigger, trigger.DoesSeeTrigger(fighter));
            }
        }

        public void RemoveTrigger(MarkTrigger trigger)
        {
            trigger.Triggered -= OnMarkTriggered;
            m_triggers.Remove(trigger);

            ContextHandler.SendGameActionFightUnmarkCellsMessage(Clients, trigger);
        }

        public void TriggerMarks(Cell cell, FightActor trigger, TriggerType triggerType)
        {
            var triggersToRemove = new List<MarkTrigger>();

            foreach (MarkTrigger markTrigger in m_triggers)
            {
                if (markTrigger.TriggerType == triggerType && markTrigger.ContainsCell(cell))
                {
                    StartSequence(SequenceTypeEnum.SEQUENCE_GLYPH_TRAP);
                    markTrigger.Trigger(trigger);

                    if (markTrigger is Trap)

                        triggersToRemove.Add(markTrigger);

                    EndSequence(SequenceTypeEnum.SEQUENCE_GLYPH_TRAP);
                }
            }

            foreach (MarkTrigger markTrigger in triggersToRemove)
            {
                RemoveTrigger(markTrigger);
            }

            CheckFightEnd();
        }

        public void DecrementGlyphDuration(FightActor caster)
        {
            var triggersToRemove = new List<MarkTrigger>();
            foreach (MarkTrigger trigger in m_triggers)
            {
                if (trigger is Glyph && (trigger as Glyph).Caster == caster)
                {
                    if ((trigger as Glyph).DecrementDuration())
                        triggersToRemove.Add(trigger);
                }
            }

            if (triggersToRemove.Count == 0)
                return;

            StartSequence(SequenceTypeEnum.SEQUENCE_GLYPH_TRAP);
            foreach (MarkTrigger trigger in triggersToRemove)
            {
                RemoveTrigger(trigger);
            }
            EndSequence(SequenceTypeEnum.SEQUENCE_GLYPH_TRAP);
        }

        public int PopNextTriggerId()
        {
            return m_triggerIdProvider.Pop();
        }

        public void FreeTriggerId(int id)
        {
            m_triggerIdProvider.Push(id);
        }

        private void OnMarkTriggered(MarkTrigger markTrigger, FightActor trigger, Spell triggerSpell)
        {
            ContextHandler.SendGameActionFightTriggerGlyphTrapMessage(Clients, markTrigger, trigger, triggerSpell);
        }

        #endregion

        #region Ready Checker

        protected virtual void OnLaggersSpotted(NamedFighter[] laggers)
        {
            if (laggers.Length == 1)
            {
                BasicHandler.SendTextInformationMessage(Clients, 1, 28, laggers[0].Name);
            }
            else if (laggers.Length > 1)
            {
                BasicHandler.SendTextInformationMessage(Clients, 1, 29, string.Join(",", laggers.Select(entry => entry.Name)));
            }
        }

        #endregion

        #region Send Methods

        protected abstract void SendGameFightJoinMessage(CharacterFighter fighter);

        #endregion

        #region Get Methods

        private readonly WorldClientCollection m_clients = new WorldClientCollection();

        /// <summary>
        /// Do not modify, just read
        /// </summary>
        public WorldClientCollection Clients
        {
            get { return m_clients; }
        }

        public IEnumerable<Character> GetAllCharacters()
        {
            return Fighters.OfType<CharacterFighter>().Select(entry => entry.Character);
        }

        public void ForEach(Action<Character> action)
        {
            foreach (Character character in GetAllCharacters())
            {
                action(character);
            }
        }

        public void ForEach(Action<Character> action, Character except)
        {
            foreach (Character character in GetAllCharacters())
            {
                if (character == except)
                    continue;

                action(character);
            }
        }

        protected abstract bool CanCancelFight();

        public bool IsCellFree(Cell cell)
        {
            return cell.Walkable && !cell.NonWalkableDuringFight && GetOneFighter(cell) == null;
        }

        public int GetFightDuration()
        {
            return !IsStarted ? 0 : (int) (DateTime.Now - StartTime).TotalMilliseconds;
        }

        public sbyte GetNextContextualId()
        {
            return (sbyte) m_contextualIdProvider.Pop();
        }

        public void FreeContextualId(sbyte id)
        {
            m_contextualIdProvider.Push(id);
        }

        public FightActor GetOneFighter(int id)
        {
            return Fighters.Where(entry => entry.Id == id).SingleOrDefault();
        }

        public FightActor GetOneFighter(Cell cell)
        {
            return Fighters.Where(entry => Equals(entry.Position.Cell, cell)).SingleOrDefault();
        }

        public FightActor GetOneFighter(Predicate<FightActor> predicate)
        {
            IEnumerable<FightActor> entries = Fighters.Where(entry => predicate(entry));

            if (entries.Count() != 0)
                return null;

            return entries.SingleOrDefault();
        }

        public T GetOneFighter<T>(int id) where T : FightActor
        {
            return Fighters.OfType<T>().Where(entry => entry.Id == id).SingleOrDefault();
        }

        public T GetOneFighter<T>(Cell cell) where T : FightActor
        {
            return Fighters.OfType<T>().Where(entry => Equals(entry.Position.Cell, cell)).SingleOrDefault();
        }

        public T GetOneFighter<T>(Predicate<T> predicate) where T : FightActor
        {
            return Fighters.OfType<T>().Where(entry => predicate(entry)).SingleOrDefault();
        }

        public T GetFirstFighter<T>(int id) where T : FightActor
        {
            return Fighters.OfType<T>().Where(entry => entry.Id == id).FirstOrDefault();
        }

        public T GetFirstFighter<T>(Cell cell) where T : FightActor
        {
            return Fighters.OfType<T>().Where(entry => Equals(entry.Position.Cell, cell)).FirstOrDefault();
        }

        public T GetFirstFighter<T>(Predicate<T> predicate) where T : FightActor
        {
            return Fighters.OfType<T>().Where(entry => predicate(entry)).FirstOrDefault();
        }

        public IEnumerable<FightActor> GetAllFighters()
        {
            return Fighters;
        }

        public IEnumerable<FightActor> GetLeavers()
        {
            return Leavers;
        }

        public IEnumerable<FightActor> GetAllFightersAndLeavers()
        {
            return Fighters.Concat(Leavers);
        }

        public IEnumerable<FightActor> GetAllFighters(Cell[] cells)
        {
            return GetAllFighters<FightActor>(entry => cells.Contains(entry.Position.Cell));
        }

        public IEnumerable<FightActor> GetAllFighters(IEnumerable<Cell> cells)
        {
            return GetAllFighters(cells.ToArray());
        }

        public IEnumerable<FightActor> GetAllFighters(Predicate<FightActor> predicate)
        {
            return Fighters.Where(entry => predicate(entry));
        }

        public IEnumerable<T> GetAllFighters<T>() where T : FightActor
        {
            return Fighters.OfType<T>();
        }

        public IEnumerable<T> GetAllFighters<T>(Predicate<T> predicate) where T : FightActor
        {
            return Fighters.OfType<T>().Where(entry => predicate(entry));
        }

        public IEnumerable<int> GetDeadFightersIds()
        {
            return GetAllFightersAndLeavers().Where(entry => entry.IsDead()).Select(entry => entry.Id);
        }

        public IEnumerable<int> GetAliveFightersIds()
        {
            return GetAllFighters<FightActor>(entry => entry.IsAlive()).Select(entry => entry.Id);
        }

        public FightCommonInformations GetFightCommonInformations()
        {
            return new FightCommonInformations(Id,
                                               (sbyte) FightType,
                                               m_teams.Select(entry => entry.GetFightTeamInformations()),
                                               m_teams.Select(entry => entry.BladePosition.Cell.Id),
                                               m_teams.Select(entry => entry.GetFightOptionsInformations()));
        }

        #endregion
    }
}