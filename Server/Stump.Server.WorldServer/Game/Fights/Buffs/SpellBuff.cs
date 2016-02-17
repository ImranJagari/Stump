using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Spells;

namespace Stump.Server.WorldServer.Game.Fights.Buffs
{
    public class SpellBuff : Buff
    {
        public SpellBuff(int id, FightActor target, FightActor caster, EffectBase effect, Spell spell, Spell boostedSpell, short boost, bool critical, bool dispelable)
            : base(id, target, caster, effect, spell, critical, dispelable)
        {
            BoostedSpell = boostedSpell;
            Boost = boost;
        }

        public SpellBuff(int id, FightActor target, FightActor caster, EffectBase effect, Spell spell, Spell boostedSpell, short boost, bool critical, bool dispelable, int priority, short customActionId)
            : base(id, target, caster, effect, spell, critical, dispelable, priority, customActionId)
        {
            BoostedSpell = boostedSpell;
            Boost = boost;
        }

        public Spell BoostedSpell
        {
            get;
        }

        public short Boost
        {
            get;
        }

        public override void Apply()
        {
            base.Apply();
            Target.BuffSpell(BoostedSpell, Boost);
        }

        public override void Dispell()
        {
            base.Dispell();
            Target.UnBuffSpell(BoostedSpell, Boost);
        }

        public override AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            if (Delay == 0)
                return new FightTemporarySpellBoostEffect(Id, Target.Id, Duration, (sbyte)(Dispellable ? FightDispellableEnum.DISPELLABLE : FightDispellableEnum.DISPELLABLE_BY_DEATH), (short) Spell.Id, Effect.Id, 0, Boost, (short) BoostedSpell.Id);

            var values = new object[] { 0, 0, 0 };
            values = Effect.GetValues();

            return new FightTriggeredEffect(Id, Target.Id, (short)(Duration + Delay), (sbyte)(Dispellable ? FightDispellableEnum.DISPELLABLE : FightDispellableEnum.DISPELLABLE_BY_DEATH), (short)Spell.Id, Effect.Id, 0, (short)values[0], (short)values[1], (short)values[2], Delay);
        }
    }
}