// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeItemAutoCraftRemainingMessage.xml' the '05/11/2011 17:25:56'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeItemAutoCraftRemainingMessage : Message
	{
		public const uint Id = 6015;
		public override uint MessageId
		{
			get
			{
				return 6015;
			}
		}
		
		public int count;
		
		public ExchangeItemAutoCraftRemainingMessage()
		{
		}
		
		public ExchangeItemAutoCraftRemainingMessage(int count)
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
			if ( count < 0 )
			{
				throw new Exception("Forbidden value on count = " + count + ", it doesn't respect the following condition : count < 0");
			}
		}
	}
}
