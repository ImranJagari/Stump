

// Generated on 08/11/2013 11:28:59
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class InventoryContentAndPresetMessage : InventoryContentMessage
    {
        public const uint Id = 6162;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public IEnumerable<Types.Preset> presets;
        
        public InventoryContentAndPresetMessage()
        {
        }
        
        public InventoryContentAndPresetMessage(IEnumerable<Types.ObjectItem> objects, int kamas, IEnumerable<Types.Preset> presets)
         : base(objects, kamas)
        {
            this.presets = presets;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUShort((ushort)presets.Count());
            foreach (var entry in presets)
            {
                 entry.Serialize(writer);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var limit = reader.ReadUShort();
            presets = new Types.Preset[limit];
            for (int i = 0; i < limit; i++)
            {
                 (presets as Types.Preset[])[i] = new Types.Preset();
                 (presets as Types.Preset[])[i].Deserialize(reader);
            }
        }
        
        public override int GetSerializationSize()
        {
            return base.GetSerializationSize() + sizeof(short) + presets.Sum(x => x.GetSerializationSize());
        }
        
    }
    
}