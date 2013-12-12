

// Generated on 12/12/2013 16:56:55
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GameContextRemoveMultipleElementsMessage : Message
    {
        public const uint Id = 252;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public IEnumerable<int> id;
        
        public GameContextRemoveMultipleElementsMessage()
        {
        }
        
        public GameContextRemoveMultipleElementsMessage(IEnumerable<int> id)
        {
            this.id = id;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUShort((ushort)id.Count());
            foreach (var entry in id)
            {
                 writer.WriteInt(entry);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            var limit = reader.ReadUShort();
            id = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 (id as int[])[i] = reader.ReadInt();
            }
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(short) + id.Sum(x => sizeof(int));
        }
        
    }
    
}