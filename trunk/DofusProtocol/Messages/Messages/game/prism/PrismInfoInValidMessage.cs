// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PrismInfoInValidMessage.xml' the '24/06/2011 12:04:56'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PrismInfoInValidMessage : Message
	{
		public const uint Id = 5859;
		public override uint MessageId
		{
			get
			{
				return 5859;
			}
		}
		
		public byte reason;
		
		public PrismInfoInValidMessage()
		{
		}
		
		public PrismInfoInValidMessage(byte reason)
		{
			this.reason = reason;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteByte(reason);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			reason = reader.ReadByte();
			if ( reason < 0 )
			{
				throw new Exception("Forbidden value on reason = " + reason + ", it doesn't respect the following condition : reason < 0");
			}
		}
	}
}
