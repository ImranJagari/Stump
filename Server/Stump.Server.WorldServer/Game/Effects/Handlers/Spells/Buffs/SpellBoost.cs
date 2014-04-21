﻿using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Spell = Stump.Server.WorldServer.Game.Spells.Spell;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Buffs
{
    [EffectHandler(EffectsEnum.Effect_SpellBoost)]
    public class SpellBoost : SpellEffectHandler
    {
        public SpellBoost(EffectDice effect, FightActor caster, Spell spell, Cell targetedCell, bool critical) : base(effect, caster, spell, targetedCell, critical)
        {
        }

        public override bool Apply()
        {
            foreach (var actor in GetAffectedActors())
            {
                var integerEffect = GenerateEffect();

                if (integerEffect == null)
                    return false;

                var boostedSpell = actor.GetSpell(Dice.DiceNum);

                if (boostedSpell == null)
                    return false;

                var buff = new SpellBuff(actor.PopNextBuffId(), actor, Caster, Dice, Spell, boostedSpell, Dice.Value, false, true);

                actor.AddAndApplyBuff(buff);
            }

            return true;
        }
    }
}