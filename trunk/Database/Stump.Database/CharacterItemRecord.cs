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
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;
using Stump.DofusProtocol.Enums;

namespace Stump.Database
{
    [AttributeDatabase(DatabaseService.WorldServer)]
    [ActiveRecord("characters_items")]
    public sealed class CharacterItemRecord : ActiveRecordBase<CharacterItemRecord>
    {
        public const int MAX_EFFECT_PER_ITEM = 32;

        [PrimaryKey(PrimaryKeyType.Native, "Guid")]
        public long Guid
        {
            get;
            set;
        }

        [Property("OwnerId")]
        public long OwnerId
        {
            get;
            set;
        }

        [Property("ItemId")]
        public int ItemId
        {
            get;
            set;
        }

        [Property("Stack")]
        public uint Stack
        {
            get;
            set;
        }

        [Property("Position")]
        public CharacterInventoryPositionEnum Position
        {
            get;
            set;
        }

        [Property("Effects", Length = MAX_EFFECT_PER_ITEM*256)] // Max Effects.Length = 32 (32 effets per item)
            public List<byte[]> Effects
        {
            get;
            set;
        }


        public static CharacterItemRecord FindItemByGuid(long guid)
        {
            return FindByPrimaryKey(guid);
        }

        public static CharacterItemRecord[] FindItemsByCharacter(long characterId)
        {
            return FindAll(Restrictions.Eq("OwnerId", characterId));
        }

        public static CharacterItemRecord FindItemByCharacterAndPosition(long characterId, CharacterInventoryPositionEnum position)
        {
            return FindOne(Restrictions.Eq("OwnerId", characterId), Restrictions.Eq("Position", position));
        }

        public static CharacterItemRecord[] FindItemsByCharacterAndPosition(long characterId, params CharacterInventoryPositionEnum[] positions)
        {
            return FindAll(Restrictions.Eq("OwnerId", characterId), Restrictions.In("Position", positions));
        }
    }
}