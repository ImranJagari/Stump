

// Generated on 12/20/2015 16:37:08
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class PrismsListUpdateMessage : PrismsListMessage
    {
        public const uint Id = 6438;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public PrismsListUpdateMessage()
        {
        }
        
        public PrismsListUpdateMessage(IEnumerable<Types.PrismSubareaEmptyInfo> prisms)
         : base(prisms)
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }
        
    }
    
}