﻿using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Spells;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Move
{
    [EffectHandler(EffectsEnum.Effect_Retreat)]
    public class Retreat : Push
    {
        public Retreat(EffectDice effect, FightActor caster, Spell spell, Cell targetedCell, bool critical)
            : base(effect, caster, spell, targetedCell, critical)
        {
        }

        public override bool Apply()
        {
            AddAffectedActor(Caster);

            var oppositeDirection = TargetedPoint.OrientationTo(Caster.Position.Point);

            TargetedPoint = Caster.Position.Point.GetNearestCellInDirection(oppositeDirection);
            TargetedCell = Map.Cells[TargetedPoint.CellId];

            return base.Apply();
        }
    }
}
