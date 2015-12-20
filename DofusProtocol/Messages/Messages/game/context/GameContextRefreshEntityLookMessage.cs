

// Generated on 12/20/2015 16:36:48
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GameContextRefreshEntityLookMessage : Message
    {
        public const uint Id = 5637;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public double id;
        public Types.EntityLook look;
        
        public GameContextRefreshEntityLookMessage()
        {
        }
        
        public GameContextRefreshEntityLookMessage(double id, Types.EntityLook look)
        {
            this.id = id;
            this.look = look;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(id);
            look.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            id = reader.ReadDouble();
            if (id < -9.007199254740992E15 || id > 9.007199254740992E15)
                throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < -9.007199254740992E15 || id > 9.007199254740992E15");
            look = new Types.EntityLook();
            look.Deserialize(reader);
        }
        
    }
    
}