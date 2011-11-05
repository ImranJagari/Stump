// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'HumanWithGuildInformations.xml' the '05/11/2011 17:26:02'
using System;
using Stump.Core.IO;
using System.Collections.Generic;

namespace Stump.DofusProtocol.Types
{
	public class HumanWithGuildInformations : HumanInformations
	{
		public const uint Id = 153;
		public override short TypeId
		{
			get
			{
				return 153;
			}
		}
		
		public Types.GuildInformations guildInformations;
		
		public HumanWithGuildInformations()
		{
		}
		
		public HumanWithGuildInformations(IEnumerable<Types.EntityLook> followingCharactersLook, sbyte emoteId, double emoteStartTime, Types.ActorRestrictionsInformations restrictions, short titleId, string titleParam, Types.GuildInformations guildInformations)
			 : base(followingCharactersLook, emoteId, emoteStartTime, restrictions, titleId, titleParam)
		{
			this.guildInformations = guildInformations;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			guildInformations.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			guildInformations = new Types.GuildInformations();
			guildInformations.Deserialize(reader);
		}
	}
}
