

// Generated on 09/01/2015 10:48:04
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GameMapMovementConfirmMessage : Message
    {
        public const uint Id = 952;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public GameMapMovementConfirmMessage()
        {
        }
        
        
        public override void Serialize(IDataWriter writer)
        {
        }
        
        public override void Deserialize(IDataReader reader)
        {
        }
        
    }
    
}