// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'DungeonPartyFinderRoomContentMessage.xml' the '05/11/2011 17:25:52'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class DungeonPartyFinderRoomContentMessage : Message
	{
		public const uint Id = 6247;
		public override uint MessageId
		{
			get
			{
				return 6247;
			}
		}
		
		public short dungeonId;
		public IEnumerable<Types.DungeonPartyFinderPlayer> players;
		
		public DungeonPartyFinderRoomContentMessage()
		{
		}
		
		public DungeonPartyFinderRoomContentMessage(short dungeonId, IEnumerable<Types.DungeonPartyFinderPlayer> players)
		{
			this.dungeonId = dungeonId;
			this.players = players;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(dungeonId);
			writer.WriteUShort((ushort)players.Count());
			foreach (var entry in players)
			{
				entry.Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			dungeonId = reader.ReadShort();
			if ( dungeonId < 0 )
			{
				throw new Exception("Forbidden value on dungeonId = " + dungeonId + ", it doesn't respect the following condition : dungeonId < 0");
			}
			int limit = reader.ReadUShort();
			players = new Types.DungeonPartyFinderPlayer[limit];
			for (int i = 0; i < limit; i++)
			{
				(players as Types.DungeonPartyFinderPlayer[])[i] = new Types.DungeonPartyFinderPlayer();
				(players as Types.DungeonPartyFinderPlayer[])[i].Deserialize(reader);
			}
		}
	}
}
