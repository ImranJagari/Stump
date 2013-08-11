

// Generated on 08/11/2013 11:29:04
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class PrismFightDefendersSwapMessage : Message
    {
        public const uint Id = 5902;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public double fightId;
        public int fighterId1;
        public int fighterId2;
        
        public PrismFightDefendersSwapMessage()
        {
        }
        
        public PrismFightDefendersSwapMessage(double fightId, int fighterId1, int fighterId2)
        {
            this.fightId = fightId;
            this.fighterId1 = fighterId1;
            this.fighterId2 = fighterId2;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(fightId);
            writer.WriteInt(fighterId1);
            writer.WriteInt(fighterId2);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            fightId = reader.ReadDouble();
            fighterId1 = reader.ReadInt();
            if (fighterId1 < 0)
                throw new Exception("Forbidden value on fighterId1 = " + fighterId1 + ", it doesn't respect the following condition : fighterId1 < 0");
            fighterId2 = reader.ReadInt();
            if (fighterId2 < 0)
                throw new Exception("Forbidden value on fighterId2 = " + fighterId2 + ", it doesn't respect the following condition : fighterId2 < 0");
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(double) + sizeof(int) + sizeof(int);
        }
        
    }
    
}