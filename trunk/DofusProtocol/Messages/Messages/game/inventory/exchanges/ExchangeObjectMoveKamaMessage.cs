// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeObjectMoveKamaMessage.xml' the '24/06/2011 12:04:54'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeObjectMoveKamaMessage : Message
	{
		public const uint Id = 5520;
		public override uint MessageId
		{
			get
			{
				return 5520;
			}
		}
		
		public int quantity;
		
		public ExchangeObjectMoveKamaMessage()
		{
		}
		
		public ExchangeObjectMoveKamaMessage(int quantity)
		{
			this.quantity = quantity;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(quantity);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			quantity = reader.ReadInt();
		}
	}
}
