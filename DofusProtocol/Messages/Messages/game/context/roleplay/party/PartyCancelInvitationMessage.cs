

// Generated on 11/16/2015 14:26:10
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class PartyCancelInvitationMessage : AbstractPartyMessage
    {
        public const uint Id = 6254;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int guestId;
        
        public PartyCancelInvitationMessage()
        {
        }
        
        public PartyCancelInvitationMessage(int partyId, int guestId)
         : base(partyId)
        {
            this.guestId = guestId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarInt(guestId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            guestId = reader.ReadVarInt();
            if (guestId < 0)
                throw new Exception("Forbidden value on guestId = " + guestId + ", it doesn't respect the following condition : guestId < 0");
        }
        
    }
    
}