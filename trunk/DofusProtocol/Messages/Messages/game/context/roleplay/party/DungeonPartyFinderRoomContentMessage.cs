// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'DungeonPartyFinderRoomContentMessage.xml' the '24/06/2011 12:04:51'
using System;
using Stump.Core.IO;

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
		public Types.DungeonPartyFinderPlayer[] players;
		
		public DungeonPartyFinderRoomContentMessage()
		{
		}
		
		public DungeonPartyFinderRoomContentMessage(short dungeonId, Types.DungeonPartyFinderPlayer[] players)
		{
			this.dungeonId = dungeonId;
			this.players = players;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(dungeonId);
			writer.WriteUShort((ushort)players.Length);
			for (int i = 0; i < players.Length; i++)
			{
				players[i].Serialize(writer);
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
				players[i] = new Types.DungeonPartyFinderPlayer();
				players[i].Deserialize(reader);
			}
		}
	}
}
