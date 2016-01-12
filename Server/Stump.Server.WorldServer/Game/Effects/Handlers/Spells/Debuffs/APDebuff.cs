﻿using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Handlers.Actions;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Debuffs
{
    [EffectHandler(EffectsEnum.Effect_SubAP)]
    public class APDebuff: SpellEffectHandler
    {
        public APDebuff(EffectDice effect, FightActor caster, Spell spell, Cell targetedCell, bool critical)
            : base(effect, caster, spell, targetedCell, critical)
        {
        }

        public override bool Apply()
        {
            foreach (var actor in GetAffectedActors())
            {
                var integerEffect = GenerateEffect();

                if (integerEffect == null)
                    return false;

                var target = actor;
                var buff = actor.GetBestReflectionBuff();
                if (buff != null && buff.ReflectedLevel >= Spell.CurrentLevel && Spell.Template.Id != 0)
                {
                    NotifySpellReflected(actor);
                    target = Caster;

                    if (buff.Duration <= 0)
                        actor.RemoveBuff(buff);
                }

                if (Effect.Duration != 0)
                {
                    AddStatBuff(target, (short)(-(integerEffect.Value)), PlayerFields.AP, true, (short)EffectsEnum.Effect_SubAP);
                    actor.OnFightPointsVariation(ActionsEnum.ACTION_CHARACTER_ACTION_POINTS_LOST, Caster, actor, (short)(-integerEffect.Value));
                }
                else
                {
                    target.LostAP(integerEffect.Value, Caster);
                }


                target.TriggerBuffs(target, BuffTriggerType.OnAPLost);
            }

            return true;
        }
        private void NotifySpellReflected(FightActor source)
        {
            ActionsHandler.SendGameActionFightReflectSpellMessage(Fight.Clients, Caster, source);
        }
    }
}
