// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeObjectMessage.xml' the '05/11/2011 17:25:57'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeObjectMessage : Message
	{
		public const uint Id = 5515;
		public override uint MessageId
		{
			get
			{
				return 5515;
			}
		}
		
		public bool remote;
		
		public ExchangeObjectMessage()
		{
		}
		
		public ExchangeObjectMessage(bool remote)
		{
			this.remote = remote;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteBoolean(remote);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			remote = reader.ReadBoolean();
		}
	}
}
