// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'DungeonPartyFinderRoomContentUpdateMessage.xml' the '24/06/2011 12:04:51'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class DungeonPartyFinderRoomContentUpdateMessage : Message
	{
		public const uint Id = 6250;
		public override uint MessageId
		{
			get
			{
				return 6250;
			}
		}
		
		public short dungeonId;
		public Types.DungeonPartyFinderPlayer[] addedPlayers;
		public int[] removedPlayersIds;
		
		public DungeonPartyFinderRoomContentUpdateMessage()
		{
		}
		
		public DungeonPartyFinderRoomContentUpdateMessage(short dungeonId, Types.DungeonPartyFinderPlayer[] addedPlayers, int[] removedPlayersIds)
		{
			this.dungeonId = dungeonId;
			this.addedPlayers = addedPlayers;
			this.removedPlayersIds = removedPlayersIds;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(dungeonId);
			writer.WriteUShort((ushort)addedPlayers.Length);
			for (int i = 0; i < addedPlayers.Length; i++)
			{
				addedPlayers[i].Serialize(writer);
			}
			writer.WriteUShort((ushort)removedPlayersIds.Length);
			for (int i = 0; i < removedPlayersIds.Length; i++)
			{
				writer.WriteInt(removedPlayersIds[i]);
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
			addedPlayers = new Types.DungeonPartyFinderPlayer[limit];
			for (int i = 0; i < limit; i++)
			{
				addedPlayers[i] = new Types.DungeonPartyFinderPlayer();
				addedPlayers[i].Deserialize(reader);
			}
			limit = reader.ReadUShort();
			removedPlayersIds = new int[limit];
			for (int i = 0; i < limit; i++)
			{
				removedPlayersIds[i] = reader.ReadInt();
			}
		}
	}
}
