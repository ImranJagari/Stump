// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeShopStockMovementRemovedMessage.xml' the '24/06/2011 12:04:54'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeShopStockMovementRemovedMessage : Message
	{
		public const uint Id = 5907;
		public override uint MessageId
		{
			get
			{
				return 5907;
			}
		}
		
		public int objectId;
		
		public ExchangeShopStockMovementRemovedMessage()
		{
		}
		
		public ExchangeShopStockMovementRemovedMessage(int objectId)
		{
			this.objectId = objectId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(objectId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			objectId = reader.ReadInt();
			if ( objectId < 0 )
			{
				throw new Exception("Forbidden value on objectId = " + objectId + ", it doesn't respect the following condition : objectId < 0");
			}
		}
	}
}
