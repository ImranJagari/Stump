﻿using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Game.Fights
{
    public class FightTaxCollectorTeam : FightTeamWithLeader<TaxCollectorFighter>
    {
        public FightTaxCollectorTeam(sbyte id, Cell[] placementCells) : base(id, placementCells)
        {
        }

        public FightTaxCollectorTeam(sbyte id, Cell[] placementCells, AlignmentSideEnum alignmentSide)
            : base(id, placementCells, alignmentSide)
        {
        }

        public override TeamTypeEnum TeamType
        {
            get { return TeamTypeEnum.TEAM_TYPE_TAXCOLLECTOR; }
        }

        public override FighterRefusedReasonEnum CanJoin(Character character)
        {
            if (!character.IsGameMaster() && character.Guild != Leader.TaxCollectorNpc.Guild)
                return FighterRefusedReasonEnum.WRONG_GUILD;

            return base.CanJoin(character);
        }
    }
}