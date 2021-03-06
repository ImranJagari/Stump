

// Generated on 10/30/2016 16:20:49
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class PrismsListRegisterMessage : Message
    {
        public const uint Id = 6441;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public sbyte listen;
        
        public PrismsListRegisterMessage()
        {
        }
        
        public PrismsListRegisterMessage(sbyte listen)
        {
            this.listen = listen;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(listen);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            listen = reader.ReadSByte();
            if (listen < 0)
                throw new Exception("Forbidden value on listen = " + listen + ", it doesn't respect the following condition : listen < 0");
        }
        
    }
    
}