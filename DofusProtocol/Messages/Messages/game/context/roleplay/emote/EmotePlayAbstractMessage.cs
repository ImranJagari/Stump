

// Generated on 03/02/2014 20:42:40
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class EmotePlayAbstractMessage : Message
    {
        public const uint Id = 5690;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public sbyte emoteId;
        public double emoteStartTime;
        
        public EmotePlayAbstractMessage()
        {
        }
        
        public EmotePlayAbstractMessage(sbyte emoteId, double emoteStartTime)
        {
            this.emoteId = emoteId;
            this.emoteStartTime = emoteStartTime;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(emoteId);
            writer.WriteDouble(emoteStartTime);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            emoteId = reader.ReadSByte();
            if (emoteId < 0)
                throw new Exception("Forbidden value on emoteId = " + emoteId + ", it doesn't respect the following condition : emoteId < 0");
            emoteStartTime = reader.ReadDouble();
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(sbyte) + sizeof(double);
        }
        
    }
    
}