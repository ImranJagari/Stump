
// Generated on 03/25/2013 19:24:25
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class TitleSelectErrorMessage : Message
    {
        public const uint Id = 6373;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public sbyte reason;
        
        public TitleSelectErrorMessage()
        {
        }
        
        public TitleSelectErrorMessage(sbyte reason)
        {
            this.reason = reason;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(reason);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            reason = reader.ReadSByte();
            if (reason < 0)
                throw new Exception("Forbidden value on reason = " + reason + ", it doesn't respect the following condition : reason < 0");
        }
        
    }
    
}