// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'MapRunningFightListMessage.xml' the '05/11/2011 17:25:50'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class MapRunningFightListMessage : Message
	{
		public const uint Id = 5743;
		public override uint MessageId
		{
			get
			{
				return 5743;
			}
		}
		
		public IEnumerable<Types.FightExternalInformations> fights;
		
		public MapRunningFightListMessage()
		{
		}
		
		public MapRunningFightListMessage(IEnumerable<Types.FightExternalInformations> fights)
		{
			this.fights = fights;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)fights.Count());
			foreach (var entry in fights)
			{
				entry.Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			fights = new Types.FightExternalInformations[limit];
			for (int i = 0; i < limit; i++)
			{
				(fights as Types.FightExternalInformations[])[i] = new Types.FightExternalInformations();
				(fights as Types.FightExternalInformations[])[i].Deserialize(reader);
			}
		}
	}
}
