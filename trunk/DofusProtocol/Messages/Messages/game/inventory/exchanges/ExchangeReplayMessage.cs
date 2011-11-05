// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeReplayMessage.xml' the '05/11/2011 17:25:57'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeReplayMessage : Message
	{
		public const uint Id = 6002;
		public override uint MessageId
		{
			get
			{
				return 6002;
			}
		}
		
		public int count;
		
		public ExchangeReplayMessage()
		{
		}
		
		public ExchangeReplayMessage(int count)
		{
			this.count = count;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(count);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			count = reader.ReadInt();
		}
	}
}
