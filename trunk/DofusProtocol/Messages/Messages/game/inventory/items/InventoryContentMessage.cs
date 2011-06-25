// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'InventoryContentMessage.xml' the '24/06/2011 12:04:55'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class InventoryContentMessage : Message
	{
		public const uint Id = 3016;
		public override uint MessageId
		{
			get
			{
				return 3016;
			}
		}
		
		public Types.ObjectItem[] objects;
		public int kamas;
		
		public InventoryContentMessage()
		{
		}
		
		public InventoryContentMessage(Types.ObjectItem[] objects, int kamas)
		{
			this.objects = objects;
			this.kamas = kamas;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)objects.Length);
			for (int i = 0; i < objects.Length; i++)
			{
				objects[i].Serialize(writer);
			}
			writer.WriteInt(kamas);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			objects = new Types.ObjectItem[limit];
			for (int i = 0; i < limit; i++)
			{
				objects[i] = new Types.ObjectItem();
				objects[i].Deserialize(reader);
			}
			kamas = reader.ReadInt();
			if ( kamas < 0 )
			{
				throw new Exception("Forbidden value on kamas = " + kamas + ", it doesn't respect the following condition : kamas < 0");
			}
		}
	}
}
