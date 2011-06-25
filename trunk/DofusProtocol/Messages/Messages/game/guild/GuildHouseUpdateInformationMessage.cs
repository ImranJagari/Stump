// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GuildHouseUpdateInformationMessage.xml' the '24/06/2011 12:04:52'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GuildHouseUpdateInformationMessage : Message
	{
		public const uint Id = 6181;
		public override uint MessageId
		{
			get
			{
				return 6181;
			}
		}
		
		public Types.HouseInformationsForGuild housesInformations;
		
		public GuildHouseUpdateInformationMessage()
		{
		}
		
		public GuildHouseUpdateInformationMessage(Types.HouseInformationsForGuild housesInformations)
		{
			this.housesInformations = housesInformations;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			housesInformations.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			housesInformations = new Types.HouseInformationsForGuild();
			housesInformations.Deserialize(reader);
		}
	}
}
