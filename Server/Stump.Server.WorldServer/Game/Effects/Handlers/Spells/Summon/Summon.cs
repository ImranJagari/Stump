﻿using NLog;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights.Triggers;
using Stump.Server.WorldServer.Handlers.Actions;
using Stump.Server.WorldServer.Game.Spells.Casts;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Summon
{
    [EffectHandler(EffectsEnum.Effect_Summon)]
    public class Summon : SpellEffectHandler
    {
        static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public Summon(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical)
            : base(effect, caster, castHandler, targetedCell, critical)
        {
        }

        protected override bool InternalApply()
        {
            var monster = MonsterManager.Instance.GetMonsterGrade(Dice.DiceNum, Dice.DiceFace);

            if (monster == null)
            {
                logger.Error("Cannot summon monster {0} grade {1} (not found)", Dice.DiceNum, Dice.DiceFace);
                return false;
            }

            if (monster.Template.UseSummonSlot && !Caster.CanSummon())
                return false;

            if (monster.Template.Id == 3287 || monster.Template.Id == 3288 || monster.Template.Id == 3289)
            {
                var summon = new SummonedTurret(Fight.GetNextContextualId(), Caster, monster, Spell, TargetedCell) {SummoningEffect = this};
                ActionsHandler.SendGameActionFightSummonMessage(Fight.Clients, summon);

                Caster.AddSummon(summon);
                Caster.Team.AddFighter(summon);

                Fight.TriggerMarks(summon.Cell, summon, TriggerType.MOVE);
            }
            else
            {
                var summon = new SummonedMonster(Fight.GetNextContextualId(), Caster.Team, Caster, monster, TargetedCell) {SummoningEffect = this};

                ActionsHandler.SendGameActionFightSummonMessage(Fight.Clients, summon);

                Caster.AddSummon(summon);
                Caster.Team.AddFighter(summon);

                Fight.TriggerMarks(summon.Cell, summon, TriggerType.MOVE);
            }


            return true;
        }
    }
}