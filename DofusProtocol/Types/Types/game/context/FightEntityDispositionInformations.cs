

// Generated on 11/16/2015 14:20:20
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
        
        public int carryingCharacterId;
        
        public FightEntityDispositionInformations()
        {
        }
        
        public FightEntityDispositionInformations(short cellId, sbyte direction, int carryingCharacterId)
         : base(cellId, direction)
        {
            this.carryingCharacterId = carryingCharacterId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(carryingCharacterId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            carryingCharacterId = reader.ReadInt();
        }
        
        
    }
    
}