

// Generated on 09/01/2015 10:48:20
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class InteractiveUsedMessage : Message
    {
        public const uint Id = 5745;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int entityId;
        public int elemId;
        public short skillId;
        public short duration;
        
        public InteractiveUsedMessage()
        {
        }
        
        public InteractiveUsedMessage(int entityId, int elemId, short skillId, short duration)
        {
            this.entityId = entityId;
            this.elemId = elemId;
            this.skillId = skillId;
            this.duration = duration;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(entityId);
            writer.WriteVarInt(elemId);
            writer.WriteVarShort(skillId);
            writer.WriteVarShort(duration);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            entityId = reader.ReadVarInt();
            if (entityId < 0)
                throw new Exception("Forbidden value on entityId = " + entityId + ", it doesn't respect the following condition : entityId < 0");
            elemId = reader.ReadVarInt();
            if (elemId < 0)
                throw new Exception("Forbidden value on elemId = " + elemId + ", it doesn't respect the following condition : elemId < 0");
            skillId = reader.ReadVarShort();
            if (skillId < 0)
                throw new Exception("Forbidden value on skillId = " + skillId + ", it doesn't respect the following condition : skillId < 0");
            duration = reader.ReadVarShort();
            if (duration < 0)
                throw new Exception("Forbidden value on duration = " + duration + ", it doesn't respect the following condition : duration < 0");
        }
        
    }
    
}