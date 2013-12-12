

// Generated on 12/12/2013 16:57:25
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class SystemMessageDisplayMessage : Message
    {
        public const uint Id = 189;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public bool hangUp;
        public short msgId;
        public IEnumerable<string> parameters;
        
        public SystemMessageDisplayMessage()
        {
        }
        
        public SystemMessageDisplayMessage(bool hangUp, short msgId, IEnumerable<string> parameters)
        {
            this.hangUp = hangUp;
            this.msgId = msgId;
            this.parameters = parameters;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(hangUp);
            writer.WriteShort(msgId);
            writer.WriteUShort((ushort)parameters.Count());
            foreach (var entry in parameters)
            {
                 writer.WriteUTF(entry);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            hangUp = reader.ReadBoolean();
            msgId = reader.ReadShort();
            if (msgId < 0)
                throw new Exception("Forbidden value on msgId = " + msgId + ", it doesn't respect the following condition : msgId < 0");
            var limit = reader.ReadUShort();
            parameters = new string[limit];
            for (int i = 0; i < limit; i++)
            {
                 (parameters as string[])[i] = reader.ReadUTF();
            }
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(bool) + sizeof(short) + sizeof(short) + parameters.Sum(x => sizeof(short) + Encoding.UTF8.GetByteCount(x));
        }
        
    }
    
}