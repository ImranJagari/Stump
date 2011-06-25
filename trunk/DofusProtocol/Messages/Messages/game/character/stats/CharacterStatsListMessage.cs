// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharacterStatsListMessage.xml' the '24/06/2011 12:04:47'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class CharacterStatsListMessage : Message
	{
		public const uint Id = 500;
		public override uint MessageId
		{
			get
			{
				return 500;
			}
		}
		
		public Types.CharacterCharacteristicsInformations stats;
		
		public CharacterStatsListMessage()
		{
		}
		
		public CharacterStatsListMessage(Types.CharacterCharacteristicsInformations stats)
		{
			this.stats = stats;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			stats.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			stats = new Types.CharacterCharacteristicsInformations();
			stats.Deserialize(reader);
		}
	}
}
