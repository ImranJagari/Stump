

// Generated on 10/30/2016 16:20:35
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class PartyStopFollowRequestMessage : AbstractPartyMessage
    {
        public const uint Id = 5574;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public long playerId;
        
        public PartyStopFollowRequestMessage()
        {
        }
        
        public PartyStopFollowRequestMessage(int partyId, long playerId)
         : base(partyId)
        {
            this.playerId = playerId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarLong(playerId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            playerId = reader.ReadVarLong();
            if (playerId < 0 || playerId > 9007199254740990)
                throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0 || playerId > 9007199254740990");
        }
        
    }
    
}