// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ObjectsQuantityMessage.xml' the '05/11/2011 17:25:58'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class ObjectsQuantityMessage : Message
	{
		public const uint Id = 6206;
		public override uint MessageId
		{
			get
			{
				return 6206;
			}
		}
		
		public IEnumerable<Types.ObjectItemQuantity> objectsUIDAndQty;
		
		public ObjectsQuantityMessage()
		{
		}
		
		public ObjectsQuantityMessage(IEnumerable<Types.ObjectItemQuantity> objectsUIDAndQty)
		{
			this.objectsUIDAndQty = objectsUIDAndQty;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)objectsUIDAndQty.Count());
			foreach (var entry in objectsUIDAndQty)
			{
				entry.Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			objectsUIDAndQty = new Types.ObjectItemQuantity[limit];
			for (int i = 0; i < limit; i++)
			{
				(objectsUIDAndQty as Types.ObjectItemQuantity[])[i] = new Types.ObjectItemQuantity();
				(objectsUIDAndQty as Types.ObjectItemQuantity[])[i].Deserialize(reader);
			}
		}
	}
}
