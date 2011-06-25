// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GuildFightPlayersEnemiesListMessage.xml' the '24/06/2011 12:04:52'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GuildFightPlayersEnemiesListMessage : Message
	{
		public const uint Id = 5928;
		public override uint MessageId
		{
			get
			{
				return 5928;
			}
		}
		
		public double fightId;
		public Types.CharacterMinimalPlusLookInformations[] playerInfo;
		
		public GuildFightPlayersEnemiesListMessage()
		{
		}
		
		public GuildFightPlayersEnemiesListMessage(double fightId, Types.CharacterMinimalPlusLookInformations[] playerInfo)
		{
			this.fightId = fightId;
			this.playerInfo = playerInfo;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteDouble(fightId);
			writer.WriteUShort((ushort)playerInfo.Length);
			for (int i = 0; i < playerInfo.Length; i++)
			{
				playerInfo[i].Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			fightId = reader.ReadDouble();
			if ( fightId < 0 )
			{
				throw new Exception("Forbidden value on fightId = " + fightId + ", it doesn't respect the following condition : fightId < 0");
			}
			int limit = reader.ReadUShort();
			playerInfo = new Types.CharacterMinimalPlusLookInformations[limit];
			for (int i = 0; i < limit; i++)
			{
				playerInfo[i] = new Types.CharacterMinimalPlusLookInformations();
				playerInfo[i].Deserialize(reader);
			}
		}
	}
}
