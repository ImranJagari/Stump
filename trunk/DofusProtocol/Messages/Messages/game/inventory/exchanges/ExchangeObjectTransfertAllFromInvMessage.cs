// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeObjectTransfertAllFromInvMessage.xml' the '24/06/2011 12:04:54'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeObjectTransfertAllFromInvMessage : Message
	{
		public const uint Id = 6184;
		public override uint MessageId
		{
			get
			{
				return 6184;
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
