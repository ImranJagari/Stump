

// Generated on 09/01/2015 10:48:33
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class CharacterToRelookInformation : AbstractCharacterToRefurbishInformation
    {
        public const short Id = 399;
        public override short TypeId
        {
            get { return Id; }
        }
        
        
        public CharacterToRelookInformation()
        {
        }
        
        public CharacterToRelookInformation(int id, IEnumerable<int> colors, int cosmeticId)
         : base(id, colors, cosmeticId)
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }
        
        
    }
    
}