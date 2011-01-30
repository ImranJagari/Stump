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
using Stump.DofusProtocol.Classes;
using Stump.DofusProtocol.Classes.Custom;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Global;

namespace Stump.Server.WorldServer.World.Actors.Actor
{
    public abstract partial class Actor
    {

        protected Actor(long id, string name, ExtendedLook look, VectorIsometric position)
        {
            Id = id;
            Name = name;
            Look = look;
            Position = position;
        }

        protected Actor(long id, string name, ExtendedLook look, Global.Maps.Map map, ushort cellId, DirectionsEnum direction)
            : this(id, name, look, new VectorIsometric(map, cellId, direction))
        {
        }

        public readonly long Id;

        public string Name
        {
            get;
            protected set;
        }

        public VectorIsometric Position
        {
            get;
            protected set;
        }

        public ExtendedLook Look
        {
            get;
            protected set;
        }

        public Global.Maps.Map Map
        {
            get { return Position.Map; }
            protected set { Position = new VectorIsometric(value, Position); }
        }


        public EntityDispositionInformations GetIdentifiedEntityDispositionInformations()
        {
            return new IdentifiedEntityDispositionInformations(Position.CellId, (uint)Position.Direction, (int)Id);
        }

        public EntityDispositionInformations GetEntityDispositionInformations()
        {
            return new EntityDispositionInformations(Position.CellId, (uint)Position.Direction);
        }

        public virtual GameRolePlayActorInformations ToGameRolePlayActor()
        {
            return new GameRolePlayActorInformations((int)Id, Look.EntityLook, GetEntityDispositionInformations());
        }

    }
}