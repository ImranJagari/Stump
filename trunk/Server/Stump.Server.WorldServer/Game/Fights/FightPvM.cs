﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using MongoDB.Bson;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Logging;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.TaxCollectors;
using Stump.Server.WorldServer.Game.Fights.Results;
using Stump.Server.WorldServer.Game.Formulas;
using Stump.Server.WorldServer.Game.Items.TaxCollector;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Handlers.Context;

namespace Stump.Server.WorldServer.Game.Fights
{
    public class FightPvM : Fight
    {
        private bool m_ageBonusDefined;

        public FightPvM(int id, Map fightMap, FightTeam blueTeam, FightTeam redTeam)
            : base(id, fightMap, blueTeam, redTeam)
        {
        }

        public override void StartPlacement()
        {
            base.StartPlacement();

            m_placementTimer = Map.Area.CallDelayed(PlacementPhaseTime, StartFighting);
        }

        public override void StartFighting()
        {
            m_placementTimer.Dispose();

            base.StartFighting();
        }

        protected override void OnFighterAdded(FightTeam team, FightActor actor)
        {
            base.OnFighterAdded(team, actor);

            if (!team.IsMonsterTeam() || m_ageBonusDefined)
                return;

            var monsterFighter = team.Leader as MonsterFighter;
            if (monsterFighter != null)
                AgeBonus = monsterFighter.Monster.Group.AgeBonus;

            m_ageBonusDefined = true;
        }

        public override FightTypeEnum FightType
        {
            get { return FightTypeEnum.FIGHT_TYPE_PvM; }
        }

        protected override IEnumerable<IFightResult> GenerateResults()
        {
            var results = new List<IFightResult>();
            results.AddRange(GetFightersAndLeavers().Where(entry => !(entry is SummonedFighter)).Select(entry => entry.GetFightResult()));

            if (Map.TaxCollector != null)
                results.Add(new TaxCollectorFightResult(Map.TaxCollector, this));

            foreach (var team in m_teams)
            {
                IEnumerable<FightActor> droppers = ( team == RedTeam ? BlueTeam : RedTeam ).GetAllFighters(entry => entry.IsDead()).ToList();
                var looters = results.Where(x => x.CanLoot(team)).OrderByDescending(entry => entry is TaxCollectorFightResult ? -1 : entry.Prospecting); // tax collector loots at the end
                var teamPP = team.GetAllFighters().Sum(entry => entry.Stats[PlayerFields.Prospecting].Total);
                var kamas = droppers.Sum(entry => entry.GetDroppedKamas());

                foreach (var looter in looters)
                {
                    looter.Loot.Kamas = FightFormulas.AdjustDroppedKamas(looter, teamPP, kamas);

                    foreach (var item in droppers.SelectMany(dropper => dropper.RollLoot(looter)))
                    {
                        looter.Loot.AddItem(item);
                    }

                    if (looter is IExperienceResult)
                    {
                        (looter as IExperienceResult).SetEarnedExperience(FightFormulas.CalculateWinExp(looter, team.GetAllFighters(), droppers));
                    }

                    if (looter is FightPlayerResult)
                    {
                        var document = new BsonDocument
                        {
                            {"PlayerId", (looter as FightPlayerResult).Character.Id},
                            {"FightId", looter.Fight.Id},
                            {"MapId", looter.Fight.Map.Id},
                            {"FightersCount", looters.Count()},
                            {"winXP", (looter as FightPlayerResult).ExperienceData.ExperienceFightDelta},
                            {"winKamas", looter.Loot.Kamas},
                            {"winItems", looter.Loot.FightItemsString()},
                            {"Date", DateTime.Now.ToString(CultureInfo.InvariantCulture)}
                        };

                        MongoLogger.Instance.Insert("FightLoots", document);
                    }
                }
            }


            return results;
        }

        protected override void SendGameFightJoinMessage(CharacterFighter fighter)
        {
            ContextHandler.SendGameFightJoinMessage(fighter.Character.Client, true, !IsStarted, false, IsStarted, GetPlacementTimeLeft(), FightType);
        }

        protected override void SendGameFightJoinMessage(FightSpectator spectator)
        {
            ContextHandler.SendGameFightJoinMessage(spectator.Character.Client, false, !IsStarted, true, IsStarted, GetPlacementTimeLeft(), FightType);
        }

        protected override bool CanCancelFight()
        {
            return false;
        }

        public int GetPlacementTimeLeft()
        {
            var timeleft = PlacementPhaseTime - ( DateTime.Now - CreationTime ).TotalMilliseconds;

            if (timeleft < 0)
                timeleft = 0;

            return (int)timeleft;
        }

    }
}