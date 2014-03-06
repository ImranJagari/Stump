

// Generated on 03/06/2014 18:50:30
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class KrosmasterTransferMessage : Message
    {
        public const uint Id = 6348;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public string uid;
        public sbyte failure;
        
        public KrosmasterTransferMessage()
        {
        }
        
        public KrosmasterTransferMessage(string uid, sbyte failure)
        {
            this.uid = uid;
            this.failure = failure;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(uid);
            writer.WriteSByte(failure);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            uid = reader.ReadUTF();
            failure = reader.ReadSByte();
            if (failure < 0)
                throw new Exception("Forbidden value on failure = " + failure + ", it doesn't respect the following condition : failure < 0");
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(short) + Encoding.UTF8.GetByteCount(uid) + sizeof(sbyte);
        }
        
    }
    
}