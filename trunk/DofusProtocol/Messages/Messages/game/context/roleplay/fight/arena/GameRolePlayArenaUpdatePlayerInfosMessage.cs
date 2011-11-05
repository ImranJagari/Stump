// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameRolePlayArenaUpdatePlayerInfosMessage.xml' the '05/11/2011 17:25:50'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameRolePlayArenaUpdatePlayerInfosMessage : Message
	{
		public const uint Id = 6301;
		public override uint MessageId
		{
			get
			{
				return 6301;
			}
		}
		
		public short rank;
		public short bestDailyRank;
		public short bestRank;
		public short victoryCount;
		public short arenaFightcount;
		
		public GameRolePlayArenaUpdatePlayerInfosMessage()
		{
		}
		
		public GameRolePlayArenaUpdatePlayerInfosMessage(short rank, short bestDailyRank, short bestRank, short victoryCount, short arenaFightcount)
		{
			this.rank = rank;
			this.bestDailyRank = bestDailyRank;
			this.bestRank = bestRank;
			this.victoryCount = victoryCount;
			this.arenaFightcount = arenaFightcount;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(rank);
			writer.WriteShort(bestDailyRank);
			writer.WriteShort(bestRank);
			writer.WriteShort(victoryCount);
			writer.WriteShort(arenaFightcount);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			rank = reader.ReadShort();
			if ( rank < 0 )
			{
				throw new Exception("Forbidden value on rank = " + rank + ", it doesn't respect the following condition : rank < 0");
			}
			bestDailyRank = reader.ReadShort();
			if ( bestDailyRank < 0 )
			{
				throw new Exception("Forbidden value on bestDailyRank = " + bestDailyRank + ", it doesn't respect the following condition : bestDailyRank < 0");
			}
			bestRank = reader.ReadShort();
			if ( bestRank < 0 )
			{
				throw new Exception("Forbidden value on bestRank = " + bestRank + ", it doesn't respect the following condition : bestRank < 0");
			}
			victoryCount = reader.ReadShort();
			if ( victoryCount < 0 )
			{
				throw new Exception("Forbidden value on victoryCount = " + victoryCount + ", it doesn't respect the following condition : victoryCount < 0");
			}
			arenaFightcount = reader.ReadShort();
			if ( arenaFightcount < 0 )
			{
				throw new Exception("Forbidden value on arenaFightcount = " + arenaFightcount + ", it doesn't respect the following condition : arenaFightcount < 0");
			}
		}
	}
}
