

// Generated on 10/30/2016 16:20:43
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ExchangeObjectsAddedMessage : ExchangeObjectMessage
    {
        public const uint Id = 6535;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public IEnumerable<Types.ObjectItem> @object;
        
        public ExchangeObjectsAddedMessage()
        {
        }
        
        public ExchangeObjectsAddedMessage(bool remote, IEnumerable<Types.ObjectItem> @object)
         : base(remote)
        {
            this.@object = @object;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            var @object_before = writer.Position;
            var @object_count = 0;
            writer.WriteUShort(0);
            foreach (var entry in @object)
            {
                 entry.Serialize(writer);
                 @object_count++;
            }
            var @object_after = writer.Position;
            writer.Seek((int)@object_before);
            writer.WriteUShort((ushort)@object_count);
            writer.Seek((int)@object_after);

        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var limit = reader.ReadUShort();
            var @object_ = new Types.ObjectItem[limit];
            for (int i = 0; i < limit; i++)
            {
                 @object_[i] = new Types.ObjectItem();
                 @object_[i].Deserialize(reader);
            }
            @object = @object_;
        }
        
    }
    
}