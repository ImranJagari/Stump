

// Generated on 12/20/2015 17:30:55
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class FightEntityDispositionInformations : EntityDispositionInformations
    {
        public const short Id = 217;
        public override short TypeId
        {
            get { return Id; }
        }
        
        public double carryingCharacterId;
        
        public FightEntityDispositionInformations()
        {
        }
        
        public FightEntityDispositionInformations(short cellId, sbyte direction, double carryingCharacterId)
         : base(cellId, direction)
        {
            this.carryingCharacterId = carryingCharacterId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(carryingCharacterId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            carryingCharacterId = reader.ReadDouble();
            if (carryingCharacterId < -9.007199254740992E15 || carryingCharacterId > 9.007199254740992E15)
                throw new Exception("Forbidden value on carryingCharacterId = " + carryingCharacterId + ", it doesn't respect the following condition : carryingCharacterId < -9.007199254740992E15 || carryingCharacterId > 9.007199254740992E15");
        }
        
        
    }
    
}