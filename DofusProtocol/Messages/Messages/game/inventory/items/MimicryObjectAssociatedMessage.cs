

// Generated on 10/27/2014 19:57:59
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class MimicryObjectAssociatedMessage : SymbioticObjectAssociatedMessage
    {
        public const uint Id = 6462;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public MimicryObjectAssociatedMessage()
        {
        }
        
        public MimicryObjectAssociatedMessage(int hostUID)
         : base(hostUID)
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
        
        public override int GetSerializationSize()
        {
            return base.GetSerializationSize();
        }
        
    }
    
}