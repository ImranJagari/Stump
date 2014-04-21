﻿#region License GNU GPL
// SpellCastInformations.cs
// 
// Copyright (C) 2013 - BehaviorIsManaged
// 
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the Free Software Foundation;
// either version 2 of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
// See the GNU General Public License for more details. 
// You should have received a copy of the GNU General Public License along with this program; 
// if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
#endregion

using System.Collections.Generic;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Spells;

namespace Stump.Server.WorldServer.AI.Fights.Spells
{
    public class SpellCastInformations
    {
        public SpellCastInformations(Spell spell)
        {
            Spell = spell;
            Impacts = new List<SpellTarget>();
        }

        public Spell Spell
        {
            get;
            private set;
        }

        public bool IsSummoningSpell
        {
            get;
            set;
        }

        public Cell SummonCell
        {
            get;
            set;
        }

        public int MPToUse
        {
            get;
            set;
        }

        public bool HasToMove
        {
            get { return MPToUse > 0; }
        }

        public List<SpellTarget> Impacts
        {
            get;
            private set;
        }
    }
}