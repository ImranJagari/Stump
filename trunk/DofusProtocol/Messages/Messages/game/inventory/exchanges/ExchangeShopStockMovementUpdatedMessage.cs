// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeShopStockMovementUpdatedMessage.xml' the '05/11/2011 17:25:57'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeShopStockMovementUpdatedMessage : Message
	{
		public const uint Id = 5909;
		public override uint MessageId
		{
			get
			{
				return 5909;
			}
		}
		
		public Types.ObjectItemToSell objectInfo;
		
		public ExchangeShopStockMovementUpdatedMessage()
		{
		}
		
		public ExchangeShopStockMovementUpdatedMessage(Types.ObjectItemToSell objectInfo)
		{
			this.objectInfo = objectInfo;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			objectInfo.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			objectInfo = new Types.ObjectItemToSell();
			objectInfo.Deserialize(reader);
		}
	}
}
