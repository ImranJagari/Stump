

// Generated on 02/11/2015 10:20:29
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class MountToggleRidingRequestMessage : Message
    {
        public const uint Id = 5976;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public MountToggleRidingRequestMessage()
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