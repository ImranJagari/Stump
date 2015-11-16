

// Generated on 11/16/2015 14:26:22
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ExchangeStartOkNpcTradeMessage : Message
    {
        public const uint Id = 5785;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int npcId;
        
        public ExchangeStartOkNpcTradeMessage()
        {
        }
        
        public ExchangeStartOkNpcTradeMessage(int npcId)
        {
            this.npcId = npcId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(npcId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            npcId = reader.ReadInt();
        }
        
    }
    
}