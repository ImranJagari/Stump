using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Worlds.Actors.Fight;
using Stump.Server.WorldServer.Worlds.Effects.Instances;
using Stump.Server.WorldServer.Worlds.Spells;

namespace Stump.Server.WorldServer.Worlds.Fights.Buffs
{
    public abstract class Buff
    {
        public const int CHARACTERISTIC_STATE = 71;

        protected Buff(int id, FightActor target, FightActor caster, EffectBase effect, Spell spell, bool critical, bool dispelable)
        {
            Id = id;
            Target = target;
            Caster = caster;
            Effect = effect;
            Spell = spell;
            Critical = critical;
            Dispelable = dispelable;

            Duration = (short)Effect.Duration;
        }

        public int Id
        {
            get;
            private set;
        }

        public FightActor Target
        {
            get;
            private set;
        }

        public FightActor Caster
        {
            get;
            private set;
        }


        public EffectBase Effect
        {
            get;
            private set;
        }

        public Spell Spell
        {
            get;
            private set;
        }

        public short Duration
        {
            get;
            private set;
        }

        public bool Critical
        {
            get;
            private set;
        }

        public bool Dispelable
        {
            get;
            private set;
        }

        public virtual BuffType Type
        {
            get
            {
                if (Effect.Template.Characteristic == CHARACTERISTIC_STATE)
                    return BuffType.CATEGORY_STATE;

                if (Effect.Template.Operator == "-")
                    return Effect.Template.Active ? BuffType.CATEGORY_ACTIVE_MALUS : BuffType.CATEGORY_PASSIVE_MALUS;

                if (Effect.Template.Operator == "+")
                    return  Effect.Template.Active ? BuffType.CATEGORY_ACTIVE_BONUS : BuffType.CATEGORY_PASSIVE_BONUS;

                return BuffType.CATEGORY_OTHER;
            }
        }

        public abstract void Apply();
        public abstract void Remove();

        public abstract AbstractFightDispellableEffect GetAbstractFightDispellableEffect();

        /// <summary>
        /// Decrement Duration and return true whenever the buff is over
        /// </summary>
        /// <returns></returns>
        public bool DecrementDuration()
        {
            return Duration-- <= 0;
        }
    }
}