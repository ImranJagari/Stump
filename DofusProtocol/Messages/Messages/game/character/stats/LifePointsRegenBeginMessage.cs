

// Generated on 10/30/2016 16:20:24
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class LifePointsRegenBeginMessage : Message
    {
        public const uint Id = 5684;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public byte regenRate;
        
        public LifePointsRegenBeginMessage()
        {
        }
        
        public LifePointsRegenBeginMessage(byte regenRate)
        {
            this.regenRate = regenRate;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(regenRate);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            regenRate = reader.ReadByte();
            if (regenRate < 0 || regenRate > 255)
                throw new Exception("Forbidden value on regenRate = " + regenRate + ", it doesn't respect the following condition : regenRate < 0 || regenRate > 255");
        }
        
    }
    
}