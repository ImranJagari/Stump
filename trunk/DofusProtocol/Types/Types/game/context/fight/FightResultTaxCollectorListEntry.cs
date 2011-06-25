// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'FightResultTaxCollectorListEntry.xml' the '24/06/2011 12:04:58'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class FightResultTaxCollectorListEntry : FightResultFighterListEntry
	{
		public const uint Id = 84;
		public override short TypeId
		{
			get
			{
				return 84;
			}
		}
		
		public byte level;
		public Types.BasicGuildInformations guildInfo;
		public int experienceForGuild;
		
		public FightResultTaxCollectorListEntry()
		{
		}
		
		public FightResultTaxCollectorListEntry(short outcome, Types.FightLoot rewards, int id, bool alive, byte level, Types.BasicGuildInformations guildInfo, int experienceForGuild)
			 : base(outcome, rewards, id, alive)
		{
			this.level = level;
			this.guildInfo = guildInfo;
			this.experienceForGuild = experienceForGuild;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteByte(level);
			guildInfo.Serialize(writer);
			writer.WriteInt(experienceForGuild);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			level = reader.ReadByte();
			if ( level < 1 || level > 200 )
			{
				throw new Exception("Forbidden value on level = " + level + ", it doesn't respect the following condition : level < 1 || level > 200");
			}
			guildInfo = new Types.BasicGuildInformations();
			guildInfo.Deserialize(reader);
			experienceForGuild = reader.ReadInt();
		}
	}
}
