

// Generated on 02/02/2016 14:14:03
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class AllianceInvitationAnswerMessage : Message
    {
        public const uint Id = 6401;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public bool accept;
        
        public AllianceInvitationAnswerMessage()
        {
        }
        
        public AllianceInvitationAnswerMessage(bool accept)
        {
            this.accept = accept;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(accept);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            accept = reader.ReadBoolean();
        }
        
    }
    
}