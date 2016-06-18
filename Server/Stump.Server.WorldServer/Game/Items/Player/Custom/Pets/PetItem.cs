﻿using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using Stump.Core.Attributes;
using Stump.Core.Extensions;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Database.Items.Pets;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Database.Monsters;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom
{
    [ItemType(ItemTypeEnum.MONTILIER)]
    [ItemType(ItemTypeEnum.FAMILIER)]
    public sealed class PetItem : BasePlayerItem
    {
        public const EffectsEnum MealCountEffect = EffectsEnum.Effect_MealCount;

        private Dictionary<int, EffectDice> m_monsterKilledEffects;

        [Variable]
        public static int MealsPerBonus = 3;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public PetItem(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
            PetTemplate = PetManager.Instance.GetPetTemplate(Template.Id);
            MaxPower = PetTemplate.PossibleEffects.OfType<EffectDice>().Max(x => EffectManager.Instance.GetEffectMaxPower(x));

            InitializeEffects();

            if (IsEquiped())
                Owner.FightEnded += OnFightEnded;
        }

        private void InitializeEffects()
        {
            // new item
            if (Effects.OfType<EffectInteger>().All(x => x.EffectId != MealCountEffect))
            {
                Effects.Clear();
                Effects.Add(LifePointsEffect = new EffectInteger(EffectsEnum.Effect_LifePoints, Template.Effects.OfType<EffectDice>().First(x => x.EffectId == EffectsEnum.Effect_LifePoints).DiceNum));
                m_monsterKilledEffects = new Dictionary<int, EffectDice>();
            }
            else
            {
                LifePointsEffect = Effects.OfType<EffectInteger>().First(x => x.EffectId == EffectsEnum.Effect_LifePoints);
                LastMealDateEffect = Effects.OfType<EffectDate>().FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_LastMealDate);
                LastMealEffect = Effects.OfType<EffectInteger>().FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_LastMeal);
                CorpulenceEffect = Effects.OfType<EffectInteger>().FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_Corpulence);

                m_monsterKilledEffects = Effects.OfType<EffectDice>().ToDictionary(x => (int)x.DiceNum);
            }
        }

        public override bool AllowFeeding => true;

        private EffectInteger LifePointsEffect
        {
            get;
            set;
        }

        private EffectDate LastMealDateEffect
        {
            get;
            set;
        }

        private EffectInteger LastMealEffect
        {
            get;
            set;
        }

        private EffectInteger CorpulenceEffect
        {
            get;
            set;
        }

        public int LifePoints
        {
            get { return LifePointsEffect.Value; }
            set { LifePointsEffect.Value = (short)value;

                if (value <= 0)
                    Die();
            }
        }

        public DateTime? LastMealDate
        {
            get { return LastMealDateEffect?.GetDate(); }
            set
            {
                if (value == null)
                {
                    if (LastMealDateEffect == null)
                        return;

                    Effects.Remove(LastMealDateEffect);
                    LastMealDateEffect = null;
                }
                else
                {
                    if (LastMealDateEffect != null)
                        LastMealDateEffect.SetDate(value.Value);
                    else
                        Effects.Add(LastMealDateEffect = new EffectDate(EffectsEnum.Effect_LastMealDate, value.Value));
                }
            }
        }

        public int? LastMeal
        {
            get { return LastMealEffect?.Value; }
            set
            {
                if (value == null)
                {
                    if (LastMealEffect == null)
                        return;

                    Effects.Remove(LastMealEffect);
                    LastMealEffect = null;
                }
                else
                {
                    if (LastMealEffect != null)
                        LastMealEffect.Value = (short)value.Value;
                    else
                        Effects.Add(LastMealEffect = new EffectInteger(EffectsEnum.Effect_LastMeal, (short)value.Value));
                }
            }
        }

        public int? Corpulence
        {
            get { return CorpulenceEffect?.Value; }
            set
            {
                if (value == null)
                {
                    if (CorpulenceEffect == null)
                        return;

                    Effects.Remove(CorpulenceEffect);
                    CorpulenceEffect = null;
                }
                else
                {
                    if (CorpulenceEffect != null)
                        CorpulenceEffect.Value = (short)value.Value;
                    else
                        Effects.Add(CorpulenceEffect = new EffectInteger(EffectsEnum.Effect_Corpulence, (short)value.Value));
                }
            }
        }

        private int IncreaseCreatureKilledCount(MonsterTemplate monster)
        {
            EffectDice effect;

            if (!m_monsterKilledEffects.TryGetValue(monster.Id, out effect))
            {
                effect = new EffectDice((short) EffectsEnum.Effect_MonsterKilledCount, 1, (short) monster.Id, 0, new EffectBase());
                m_monsterKilledEffects.Add(monster.Id, effect);
                Effects.Add(effect);
            }
            else
                effect.Value++;

            return effect.Value;
        }

        public PetTemplate PetTemplate
        {
            get;
        }

        public double MaxPower
        {
            get;
        }

        private void Die()
        {
            ItemTemplate ghostItem; 
            if (PetTemplate.GhostItemId == null || (ghostItem = ItemManager.Instance.TryGetTemplate(PetTemplate.GhostItemId.Value)) == null)
            {
                LifePoints = 1;
                logger.Error($"Pet {PetTemplate.Id} died but has not ghost item");
                return;
            }

            var item = ItemManager.Instance.CreatePlayerItem(Owner, ghostItem, 1, Effects.Clone());
            Owner.Inventory.RemoveItem(this);
            Owner.Inventory.AddItem(item);
        }

        public override bool Feed(BasePlayerItem food)
        {
            var possibleFood = PetTemplate.Foods.FirstOrDefault(x => (x.FoodType == FoodTypeEnum.ITEM && x.FoodId == food.Template.Id) ||
                                                            (x.FoodType == FoodTypeEnum.ITEMTYPE && x.FoodId == food.Template.TypeId));

            if (possibleFood == null)
                return false;

            if (Corpulence == 3)
            {

            }

            var effectMealCount = Effects.OfType<EffectInteger>().FirstOrDefault(x => x.EffectId == MealCountEffect);

            if (effectMealCount == null)
            {
                effectMealCount = new EffectInteger(MealCountEffect, 1);
                Effects.Add(effectMealCount);
            }
            else
                effectMealCount.Value++;

            if (effectMealCount.Value % MealsPerBonus == 0)
            {
                AddBonus(possibleFood);
            }

            LastMealDate = DateTime.Now;
            LastMeal = food.Template.Id;

            Invalidate();
            Owner.Inventory.RefreshItem(this);

            return true;
        }

        private bool AddBonus(PetFoodRecord food)
        {
            var possibleEffect = PetTemplate.PossibleEffects.OfType<EffectDice>().FirstOrDefault(x => x.EffectId == food.BoostedEffect);
            var effect = Effects.OfType<EffectInteger>().FirstOrDefault(x => x.EffectId == food.BoostedEffect);

            if (possibleEffect == null)
                return false;

            if (effect?.Value >= possibleEffect.Max)
                return false;

            if (EffectManager.Instance.GetItemPower(this) >= MaxPower)
                return false;

            if (effect == null)
                Effects.Add(new EffectInteger(food.BoostedEffect, (short) food.BoostAmount));
            else
                effect.Value += (short)food.BoostAmount;

            return true;
        }

        public override bool OnEquipItem(bool unequip)
        {
            if (unequip)
                Owner.FightEnded -= OnFightEnded;
            else
                Owner.FightEnded += OnFightEnded;

            if (unequip)
                return base.OnEquipItem(true);

            if (Owner.IsRiding())
                Owner.Mount.Dismount(Owner);

            return base.OnEquipItem(false);
        }

        private void OnFightEnded(Character character, CharacterFighter fighter)
        {
            if (!fighter.Fight.IsDeathTemporarily && fighter.Fight.Losers == fighter.Team)
                LifePoints--;

            if (fighter.Fight is FightPvM)
            {
                bool fed = false;
                foreach(var monster in fighter.OpposedTeam.Fighters.OfType<MonsterFighter>().Where(x => x.IsDead()))
                {
                    var food = PetTemplate.Foods.FirstOrDefault(x => x.FoodType == FoodTypeEnum.MONSTER && x.FoodId == monster.Monster.Template.Id);

                    if (food != null)
                    {
                        if (IncreaseCreatureKilledCount(monster.Monster.Template)%food.FoodQuantity == 0)
                            AddBonus(food);

                        Invalidate();
                        fed = true;
                    }
                }

                if (fed)
                    Owner.Inventory.RefreshItem(this);
            }
        }
    }
}