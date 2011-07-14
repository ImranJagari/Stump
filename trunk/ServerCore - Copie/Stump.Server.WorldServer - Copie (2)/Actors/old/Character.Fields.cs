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
using Stump.Server.WorldServer.Breeds;
using Stump.Server.WorldServer.Dialog;
using Stump.Server.WorldServer.Exchange;
using Stump.Server.WorldServer.Global.Maps;
using Stump.Server.WorldServer.Items;
using Stump.Server.WorldServer.Npcs;

namespace Stump.Server.WorldServer.Entities
{
    public partial class Character
    {

        /// <summary>
        ///   Client associated with the character.
        /// </summary>
        public WorldClient Client
        {
            get;
            set;
        }

        /// <summary>
        ///   Record corresponding this character.
        /// </summary>
        public CharacterRecord Record
        {
            get;
            private set;
        }

        public bool InWorld
        {
            get;
            private set;
        }

        /// <summary>
        ///   Breed of this character.
        /// </summary>
        public PlayableBreedEnum BreedId
        {
            get;
            set;
        }

        /// <summary>
        ///   Sex of this character.
        /// </summary>
        public SexTypeEnum Sex
        {
            get;
            set;
        }

        public BaseBreed Breed
        {
            get { return BreedManager.GetBreed(BreedId); }
        }

        /// <summary>
        ///   Character's inventory
        /// </summary>
        public Inventory Inventory
        {
            get;
            private set;
        }

        /// <summary>
        ///   Temporary properties used to know the next map during a map transition
        /// </summary>
        public Map NextMap
        {
            get;
            set;
        }

        public IDialogRequest DialogRequest
        {
            get;
            set;
        }

        public bool IsDialogRequested
        {
            get { return DialogRequest != null; }
        }

        public Dialoger Dialoger
        {
            get;
            set;
        }

        public IDialog Dialog
        {
            get { return Dialoger.Dialog; }
        }

        public bool IsInDialog
        {
            get
            {
                return Dialoger != null;
            }
        }

        public bool IsInTrade
        {
            get { return Dialoger != null && Dialoger is Trader; }
        }

        public bool IsInDialogWithNpc
        {
            get
            {
                return Dialoger != null && Dialoger is NpcDialoger;
            }
        }

        public bool IsAway
        {
            get;
            private set;
        }

        public bool IsOccuped
        {
            get { return IsInDialog || IsDialogRequested || IsAway || IsInFight; }
        }

    }
}