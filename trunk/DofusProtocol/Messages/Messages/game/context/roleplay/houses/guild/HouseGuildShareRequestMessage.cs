// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'HouseGuildShareRequestMessage.xml' the '24/06/2011 12:04:50'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class HouseGuildShareRequestMessage : Message
	{
		public const uint Id = 5704;
		public override uint MessageId
		{
			get
			{
				return 5704;
			}
		}
		
		public bool enable;
		
		public HouseGuildShareRequestMessage()
		{
		}
		
		public HouseGuildShareRequestMessage(bool enable)
		{
			this.enable = enable;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteBoolean(enable);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			enable = reader.ReadBoolean();
		}
	}
}
