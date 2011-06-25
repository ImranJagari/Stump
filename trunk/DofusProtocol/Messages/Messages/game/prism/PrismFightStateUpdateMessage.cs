// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PrismFightStateUpdateMessage.xml' the '24/06/2011 12:04:56'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PrismFightStateUpdateMessage : Message
	{
		public const uint Id = 6040;
		public override uint MessageId
		{
			get
			{
				return 6040;
			}
		}
		
		public byte state;
		
		public PrismFightStateUpdateMessage()
		{
		}
		
		public PrismFightStateUpdateMessage(byte state)
		{
			this.state = state;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteByte(state);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			state = reader.ReadByte();
			if ( state < 0 )
			{
				throw new Exception("Forbidden value on state = " + state + ", it doesn't respect the following condition : state < 0");
			}
		}
	}
}
