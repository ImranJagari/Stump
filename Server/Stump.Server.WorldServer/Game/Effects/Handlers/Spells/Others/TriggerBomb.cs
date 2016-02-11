﻿using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights.Buffs;using Stump.Server.WorldServer.Game.Spells.Casts;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Others
{
    [EffectHandler(EffectsEnum.Effect_TriggerBomb)]
    public class TriggerBomb : SpellEffectHandler
    {
        public override int Priority => int.MaxValue;

        public TriggerBomb(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical) : base(effect, caster, castHandler, targetedCell, critical)
        {
        }

        public override bool Apply()
        {
            foreach (var bomb in GetAffectedActors(x => x is SummonedBomb && ((SummonedBomb)x).Summoner == (Caster is SlaveFighter ? ((SlaveFighter)Caster).Summoner : Caster)).Where(bomb => bomb.IsAlive()))
            {
                if (Dice.Duration != 0 || Dice.Delay != 0)
                    AddTriggerBuff(bomb, false, BuffTrigger);
                else
                    ((SummonedBomb)bomb).Explode();
            }

            return true;
        }

        static void BuffTrigger(TriggerBuff buff, FightActor triggerrer, BuffTriggerType trigger, object token)
        {
            ((SummonedBomb)buff.Target).Explode();
        }
    }
}