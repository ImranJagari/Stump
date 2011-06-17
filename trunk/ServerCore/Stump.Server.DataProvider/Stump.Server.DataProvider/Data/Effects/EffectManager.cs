﻿//// /*************************************************************************
////  *
////  *  Copyright (C) 2010 - 2011 Stump Team
////  *
////  *  This program is free software: you can redistribute it and/or modify
////  *  it under the terms of the GNU General Public License as published by
////  *  the Free Software Foundation, either version 3 of the License, or
////  *  (at your option) any later version.
////  *
////  *  This program is distributed in the hope that it will be useful,
////  *  but WITHOUT ANY WARRANTY; without even the implied warranty of
////  *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
////  *  GNU General Public License for more details.
////  *
////  *  You should have received a copy of the GNU General Public License
////  *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
////  *
////  *************************************************************************/
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Stump.DofusProtocol.Enums;
//using Stump.Server.BaseServer.Data;
//using Stump.Server.BaseServer.Initializing;
//using Stump.Server.DataProvider.Data;
//using Stump.Server.DataProvider.Data.D2oTool;
//using Stump.Server.WorldServer.Effects.Executor;
//using EffectBaseEx = Stump.DofusProtocol.D2oClasses.EffectInstance;
//using EffectCreatureEx = Stump.DofusProtocol.D2oClasses.EffectInstanceCreature;
//using EffectDateEx = Stump.DofusProtocol.D2oClasses.EffectInstanceDate;
//using EffectDiceEx = Stump.DofusProtocol.D2oClasses.EffectInstanceDice;
//using EffectDurationEx = Stump.DofusProtocol.D2oClasses.EffectInstanceDuration;
//using EffectLadderEx = Stump.DofusProtocol.D2oClasses.EffectInstanceLadder;
//using EffectMaxEx = Stump.DofusProtocol.D2oClasses.EffectInstanceMinMax;
//using EffectMountEx = Stump.DofusProtocol.D2oClasses.EffectInstanceMount;
//using EffectStringEx = Stump.DofusProtocol.D2oClasses.EffectInstanceString;
//using EffectValueEx = Stump.DofusProtocol.D2oClasses.EffectInstanceInteger;
//using EffectTemplateEx = Stump.DofusProtocol.D2oClasses.Effect;

//namespace Stump.Server.WorldServer.Effects
//{
//    public static class EffectManager
//    {
//        private static readonly Dictionary<uint, EffectTemplate> EffectTemplates = new Dictionary<uint, EffectTemplate>();

//        private static readonly Dictionary<Type, Type> LinkedEffectType = new Dictionary<Type, Type>
//            {
//                {typeof (EffectBaseEx), typeof (EffectBase)},
//                {typeof (EffectCreatureEx), typeof (EffectCreature)},
//                {typeof (EffectDateEx), typeof (EffectDate)},
//                {typeof (EffectDiceEx), typeof (EffectDice)},
//                {typeof (EffectDurationEx), typeof (EffectDuration)},
//                {typeof (EffectLadderEx), typeof (EffectLadder)},
//                {typeof (EffectMaxEx), typeof (EffectMinMax)},
//                {typeof (EffectMountEx), typeof (EffectMount)},
//                {typeof (EffectStringEx), typeof (EffectString)},
//                {typeof (EffectValueEx), typeof (EffectString)},
//            };

//        [StageStep(Stages.Three, "Loaded Effects")]
//        public static void LoadEffectsData()
//        {
//            var templatesEx = D2OLoader.LoadData<EffectTemplateEx>();

//            foreach (EffectTemplateEx template in templatesEx)
//            {
//                EffectTemplates.Add((uint) template.id, new EffectTemplate(template));
//            }

//            FightEffectExecutor.Initialize();
//        }

//        /// <summary>
//        ///   D2O Effect class to stump effect class
//        /// </summary>
//        /// <param name = "effect"></param>
//        /// <returns></returns>
//        public static EffectBase ConvertExportedEffect(EffectBaseEx effect)
//        {
//            if (!LinkedEffectType.ContainsKey(effect.GetType()))
//                return null;

//            Type type = LinkedEffectType[effect.GetType()];

//            EffectBase result;

//            try
//            {
//                result = (EffectBase) type.GetConstructor(new[] {effect.GetType()}).Invoke(new object[] {effect});
//            }
//            catch
//            {
//                return null;
//            }

//            return result;
//        }

//        public static EffectBase[] ConvertExportedEffect(IEnumerable<EffectBaseEx> effects)
//        {
//            return effects.Select(ConvertExportedEffect).ToArray();
//        }

//        public static EffectTemplate GetTemplate(uint id)
//        {
//            return !EffectTemplates.ContainsKey(id) ? null : EffectTemplates[id];
//        }

//        public static bool IsEffectRandomable(EffectsEnum effect)
//        {
//            return RandomablesEffects.Contains(effect);
//        }

//        public static EffectBaseEx GuessRealEffect(EffectBaseEx effect)
//        {
//            if (!( effect is EffectDiceEx ))
//                return effect;

//            var effectDice = effect as EffectDiceEx;

//            if (effectDice.value == 0 && effectDice.diceNum > 0 && effectDice.diceSide > 0)
//            {
//                return new EffectMaxEx
//                       {
//                           duration = effectDice.duration,
//                           effectId = effectDice.effectId,
//                           max = effectDice.diceSide,
//                           min = effectDice.diceNum,
//                           modificator = effectDice.modificator,
//                           random = effectDice.random,
//                           targetId = effectDice.targetId,
//                           trigger = effectDice.trigger,
//                           zoneShape = effectDice.zoneShape,
//                           zoneSize = effectDice.zoneSize
//                       };
//            }

//            if (effectDice.value == 0 && effectDice.diceNum == 0 && effectDice.diceSide > 0)
//            {
//                return new EffectMaxEx
//                       {
//                           duration = effectDice.duration,
//                           effectId = effectDice.effectId,
//                           max = effectDice.diceSide,
//                           min = effectDice.diceNum,
//                           modificator = effectDice.modificator,
//                           random = effectDice.random,
//                           targetId = effectDice.targetId,
//                           trigger = effectDice.trigger,
//                           zoneShape = effectDice.zoneShape,
//                           zoneSize = effectDice.zoneSize
//                       };
//            }

//            return effect;
//        }

//        #region Randomable Effects

//        /// <summary>
//        ///   Effects that are random when a new item is generated
//        /// </summary>
//        private static readonly EffectsEnum[] RandomablesEffects = new[]
//            {
//                EffectsEnum.Effect_AddMP,
//                EffectsEnum.Effect_AddGlobalDamageReduction_105,
//                EffectsEnum.Effect_AddDamageReflection,
//                EffectsEnum.Effect_AddHealth,
//                EffectsEnum.Effect_AddAP_111,
//                EffectsEnum.Effect_AddDamageBonus,
//                EffectsEnum.Effect_AddDamageMultiplicator,
//                EffectsEnum.Effect_AddCriticalHit,
//                EffectsEnum.Effect_SubRange,
//                EffectsEnum.Effect_AddRange,
//                EffectsEnum.Effect_AddStrength,
//                EffectsEnum.Effect_AddAgility,
//                EffectsEnum.Effect_AddAP_120,
//                EffectsEnum.Effect_AddDamageBonus_121,
//                EffectsEnum.Effect_AddCriticalMiss,
//                EffectsEnum.Effect_AddChance,
//                EffectsEnum.Effect_AddWisdom,
//                EffectsEnum.Effect_AddVitality,
//                EffectsEnum.Effect_AddIntelligence,
//                EffectsEnum.Effect_AddMP_128,
//                EffectsEnum.Effect_SubRange_135,
//                EffectsEnum.Effect_AddRange_136,
//                EffectsEnum.Effect_AddPhysicalDamage_137,
//                EffectsEnum.Effect_IncreaseDamage_138,
//                EffectsEnum.Effect_AddPhysicalDamage_142,
//                EffectsEnum.Effect_SubDamageBonus,
//                EffectsEnum.Effect_SubChance,
//                EffectsEnum.Effect_SubVitality,
//                EffectsEnum.Effect_SubAgility,
//                EffectsEnum.Effect_SubIntelligence,
//                EffectsEnum.Effect_SubWisdom,
//                EffectsEnum.Effect_SubStrength,
//                EffectsEnum.Effect_IncreaseWeight,
//                EffectsEnum.Effect_DecreaseWeight,
//                EffectsEnum.Effect_IncreaseAPAvoid,
//                EffectsEnum.Effect_IncreaseMPAvoid,
//                EffectsEnum.Effect_SubDodgeAPProbability,
//                EffectsEnum.Effect_SubDodgeMPProbability,
//                EffectsEnum.Effect_AddGlobalDamageReduction,
//                EffectsEnum.Effect_AddDamageBonusPercent,
//                EffectsEnum.Effect_SubAP,
//                EffectsEnum.Effect_SubMP,
//                EffectsEnum.Effect_SubCriticalHit,
//                EffectsEnum.Effect_SubMagicDamageReduction,
//                EffectsEnum.Effect_SubPhysicalDamageReduction,
//                EffectsEnum.Effect_AddInitiative,
//                EffectsEnum.Effect_SubInitiative,
//                EffectsEnum.Effect_AddProspecting,
//                EffectsEnum.Effect_SubProspecting,
//                EffectsEnum.Effect_AddHealBonus,
//                EffectsEnum.Effect_SubHealBonus,
//                EffectsEnum.Effect_AddSummonLimit,
//                EffectsEnum.Effect_AddMagicDamageReduction,
//                EffectsEnum.Effect_AddPhysicalDamageReduction,
//                EffectsEnum.Effect_SubDamageBonusPercent,
//                EffectsEnum.Effect_AddEarthResistPercent,
//                EffectsEnum.Effect_AddWaterResistPercent,
//                EffectsEnum.Effect_AddAirResistPercent,
//                EffectsEnum.Effect_AddFireResistPercent,
//                EffectsEnum.Effect_AddNeutralResistPercent,
//                EffectsEnum.Effect_SubEarthResistPercent,
//                EffectsEnum.Effect_SubWaterResistPercent,
//                EffectsEnum.Effect_SubAirResistPercent,
//                EffectsEnum.Effect_SubFireResistPercent,
//                EffectsEnum.Effect_SubNeutralResistPercent,
//                EffectsEnum.Effect_AddTrapBonus,
//                EffectsEnum.Effect_AddTrapBonusPercent,
//                EffectsEnum.Effect_AddEarthElementReduction,
//                EffectsEnum.Effect_AddWaterElementReduction,
//                EffectsEnum.Effect_AddAirElementReduction,
//                EffectsEnum.Effect_AddFireElementReduction,
//                EffectsEnum.Effect_AddNeutralElementReduction,
//                EffectsEnum.Effect_SubEarthElementReduction,
//                EffectsEnum.Effect_SubWaterElementReduction,
//                EffectsEnum.Effect_SubAirElementReduction,
//                EffectsEnum.Effect_SubFireElementReduction,
//                EffectsEnum.Effect_SubNeutralElementReduction,
//                EffectsEnum.Effect_AddPvpEarthResistPercent,
//                EffectsEnum.Effect_AddPvpWaterResistPercent,
//                EffectsEnum.Effect_AddPvpAirResistPercent,
//                EffectsEnum.Effect_AddPvpFireResistPercent,
//                EffectsEnum.Effect_AddPvpNeutralResistPercent,
//                EffectsEnum.Effect_SubPvpEarthResistPercent,
//                EffectsEnum.Effect_SubPvpWaterResistPercent,
//                EffectsEnum.Effect_SubPvpAirResistPercent,
//                EffectsEnum.Effect_SubPvpFireResistPercent,
//                EffectsEnum.Effect_SubPvpNeutralResistPercent,
//                EffectsEnum.Effect_AddPvpEarthElementReduction,
//                EffectsEnum.Effect_AddPvpWaterElementReduction,
//                EffectsEnum.Effect_AddPvpAirElementReduction,
//                EffectsEnum.Effect_AddPvpFireElementReduction,
//                EffectsEnum.Effect_AddPvpNeutralElementReduction,
//                EffectsEnum.Effect_AddGlobalDamageReduction_265,
//                EffectsEnum.Effect_AddPushDamageBonus,
//                EffectsEnum.Effect_SubPushDamageBonus,
//                EffectsEnum.Effect_AddPushDamageReduction,
//                EffectsEnum.Effect_SubPushDamageReduction,
//                EffectsEnum.Effect_AddCriticalDamageBonus,
//                EffectsEnum.Effect_SubCriticalDamageBonus,
//                EffectsEnum.Effect_AddCriticalDamageReduction,
//                EffectsEnum.Effect_SubCriticalDamageReduction,
//                EffectsEnum.Effect_AddEarthDamageBonus,
//                EffectsEnum.Effect_SubEarthDamageBonus,
//                EffectsEnum.Effect_AddFireDamageBonus,
//                EffectsEnum.Effect_SubFireDamageBonus,
//                EffectsEnum.Effect_AddWaterDamageBonus,
//                EffectsEnum.Effect_SubWaterDamageBonus,
//                EffectsEnum.Effect_AddAirDamageBonus,
//                EffectsEnum.Effect_SubAirDamageBonus,
//                EffectsEnum.Effect_AddNeutralDamageBonus,
//                EffectsEnum.Effect_SubNeutralDamageBonus,
//            };

//        #endregion
//    }
//}