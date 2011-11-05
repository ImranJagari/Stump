// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeBidHouseItemRemoveOkMessage.xml' the '05/11/2011 17:25:56'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeBidHouseItemRemoveOkMessage : Message
	{
		public const uint Id = 5946;
		public override uint MessageId
		{
			get
			{
				return 5946;
			}
		}
		
		public int sellerId;
		
		public ExchangeBidHouseItemRemoveOkMessage()
		{
		}
		
		public ExchangeBidHouseItemRemoveOkMessage(int sellerId)
		{
			this.sellerId = sellerId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(sellerId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			sellerId = reader.ReadInt();
		}
	}
}
