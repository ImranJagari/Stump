

// Generated on 02/02/2016 14:14:04
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class AuthenticationTicketRefusedMessage : Message
    {
        public const uint Id = 112;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public AuthenticationTicketRefusedMessage()
        {
        }
        
        
        public override void Serialize(IDataWriter writer)
        {
        }
        
        public override void Deserialize(IDataReader reader)
        {
        }
        
    }
    
}