// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GuildFightJoinRequestMessage.xml' the '24/06/2011 12:04:52'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GuildFightJoinRequestMessage : Message
	{
		public const uint Id = 5717;
		public override uint MessageId
		{
			get
			{
				return 5717;
			}
		}
		
		public int taxCollectorId;
		
		public GuildFightJoinRequestMessage()
		{
		}
		
		public GuildFightJoinRequestMessage(int taxCollectorId)
		{
			this.taxCollectorId = taxCollectorId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(taxCollectorId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			taxCollectorId = reader.ReadInt();
		}
	}
}
