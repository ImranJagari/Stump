// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeSellOkMessage.xml' the '05/11/2011 17:25:57'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeSellOkMessage : Message
	{
		public const uint Id = 5792;
		public override uint MessageId
		{
			get
			{
				return 5792;
			}
		}
		
		
		public override void Serialize(IDataWriter writer)
		{
		}
		
		public override void Deserialize(IDataReader reader)
		{
		}
	}
}
