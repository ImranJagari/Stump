

// Generated on 03/06/2014 18:50:25
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class MimicryObjectPreviewMessage : Message
    {
        public const uint Id = 6458;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public Types.ObjectItem result;
        
        public MimicryObjectPreviewMessage()
        {
        }
        
        public MimicryObjectPreviewMessage(Types.ObjectItem result)
        {
            this.result = result;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            result.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            result = new Types.ObjectItem();
            result.Deserialize(reader);
        }
        
        public override int GetSerializationSize()
        {
            return result.GetSerializationSize();
        }
        
    }
    
}