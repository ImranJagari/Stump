﻿// /*************************************************************************
//  *
//  *  Copyright (C) 2010 - 2011 Stump Team
//  *
//  *  This program is free software: you can redistribute it and/or modify
//  *  it under the terms of the GNU General Public License as published by
//  *  the Free Software Foundation, either version 3 of the License, or
//  *  (at your option) any later version.
//  *
//  *  This program is distributed in the hope that it will be useful,
//  *  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  *  GNU General Public License for more details.
//  *
//  *  You should have received a copy of the GNU General Public License
//  *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//  *
//  *************************************************************************/
using Stump.Database;
using Stump.Database.WorldServer;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Entities;

namespace Stump.Server.WorldServer.Items
{
    public class Weapon : Item
    {
        public Weapon(LivingEntity owner, WeaponTemplate template, long guid)
            : base(template, guid)
        {
        }

        public Weapon(LivingEntity owner, WeaponTemplate template, long guid, CharacterInventoryPositionEnum position)
            : base(template, guid, position)
        {
        }

        public Weapon(LivingEntity owner, WeaponTemplate template, long guid, CharacterInventoryPositionEnum position,
                      uint stack)
            : base(template, guid, position, stack)
        {
        }

        public Weapon(LivingEntity owner, ItemRecord record)
            : base(record)
        {
        }

        public new WeaponTemplate Template
        {
            get { return base.Template as WeaponTemplate; }
        }
    }
}