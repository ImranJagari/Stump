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
using System;
using System.Linq;
using System.Text;
using Stump.BaseCore.Framework.IO;
using Stump.DofusProtocol.Classes;
using Stump.DofusProtocol.Classes.Custom;

namespace Stump.Server.WorldServer.Entities
{
    public class CharacterRestrictions
    {

        public bool cantBeAggressed { get; set; }
        public bool cantBeChallenged { get; set; }
        public bool cantTrade { get; set; }
        public bool cantBeAttackedByMutant { get; set; }
        public bool cantRun { get; set; }
        public bool forceSlowWalk { get; set; }
        public bool cantMinimize { get; set; }
        public bool cantMove { get; set; }
        public bool cantAggress { get; set; }
        public bool cantChallenge { get; set; }
        public bool cantExchange { get; set; }
        public bool cantAttack { get; set; }
        public bool cantChat { get; set; }
        public bool cantBeMerchant { get; set; }
        public bool cantUseObject { get; set; }
        public bool cantUseTaxCollector { get; set; }
        public bool cantUseInteractive { get; set; }
        public bool cantSpeakToNPC { get; set; }
        public bool cantChangeZone { get; set; }
        public bool cantAttackMonster { get; set; }
        public bool cantWalk8Directions { get; set; }

        public string Serialize()
        {
            using(var writer = new BigEndianWriter())
            {
              var builder = new StringBuilder();
              ToActorRestrictionsInformations().serialize(writer);
               return string.Join("|", writer.Data);
            }
        }
      
        public static CharacterRestrictions Deserialize(string data)
        {
            var cr = new CharacterRestrictions();
            var r = new ActorRestrictionsInformations();
            var bytes = data.Split('|').Select(s => byte.Parse(s)).ToArray();
            r.deserializeAs_ActorRestrictionsInformations(new BigEndianReader(bytes));
            cr.cantBeAggressed = r.cantBeAggressed;
            cr.cantBeChallenged = r.cantBeChallenged;
            cr.cantTrade = r.cantTrade;
            cr.cantBeAttackedByMutant = r.cantBeAttackedByMutant;
            cr.cantRun = r.cantRun;
            cr.forceSlowWalk = r.forceSlowWalk;
            cr.cantMinimize = r.cantMinimize;
            cr.cantMove = r.cantMove;
            cr.cantAggress = r.cantAggress;
            cr.cantChallenge = r.cantChallenge;
            cr.cantExchange = r.cantExchange;
            cr.cantAttack = r.cantAttack;
            cr.cantChat = r.cantChat;
            cr.cantBeMerchant = r.cantBeMerchant;
            cr.cantUseObject = r.cantUseObject;
            cr.cantUseTaxCollector = r.cantUseTaxCollector;
            cr.cantUseInteractive = r.cantUseInteractive;
            cr.cantSpeakToNPC = r.cantSpeakToNPC;
            cr.cantChangeZone = r.cantChangeZone;
            cr.cantAttackMonster = r.cantAttackMonster;
            cr.cantWalk8Directions = r.cantWalk8Directions;

            return cr;
        }

        public ActorRestrictionsInformations ToActorRestrictionsInformations()
        {
            return new ActorRestrictionsInformations(cantBeAggressed, cantBeChallenged, cantTrade, cantBeAttackedByMutant, cantRun, forceSlowWalk, cantMinimize, cantMove, cantAggress, cantChallenge,cantExchange ,cantAttack, cantChat, cantBeMerchant, cantUseObject, cantUseTaxCollector, cantUseInteractive, cantSpeakToNPC, cantChangeZone, cantAttackMonster, cantWalk8Directions);
        }

    }
}