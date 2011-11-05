// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'DungeonPartyFinderAvailableDungeonsMessage.xml' the '05/11/2011 17:25:52'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class DungeonPartyFinderAvailableDungeonsMessage : Message
	{
		public const uint Id = 6242;
		public override uint MessageId
		{
			get
			{
				return 6242;
			}
		}
		
		public IEnumerable<short> dungeonIds;
		
		public DungeonPartyFinderAvailableDungeonsMessage()
		{
		}
		
		public DungeonPartyFinderAvailableDungeonsMessage(IEnumerable<short> dungeonIds)
		{
			this.dungeonIds = dungeonIds;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)dungeonIds.Count());
			foreach (var entry in dungeonIds)
			{
				writer.WriteShort(entry);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			dungeonIds = new short[limit];
			for (int i = 0; i < limit; i++)
			{
				(dungeonIds as short[])[i] = reader.ReadShort();
			}
		}
	}
}
