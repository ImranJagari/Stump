// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PaddockPrivateInformations.xml' the '24/06/2011 12:04:59'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class PaddockPrivateInformations : PaddockAbandonnedInformations
	{
		public const uint Id = 131;
		public override short TypeId
		{
			get
			{
				return 131;
			}
		}
		
		public Types.GuildInformations guildInfo;
		
		public PaddockPrivateInformations()
		{
		}
		
		public PaddockPrivateInformations(short maxOutdoorMount, short maxItems, int price, bool locked, int guildId, Types.GuildInformations guildInfo)
			 : base(maxOutdoorMount, maxItems, price, locked, guildId)
		{
			this.guildInfo = guildInfo;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			guildInfo.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			guildInfo = new Types.GuildInformations();
			guildInfo.Deserialize(reader);
		}
	}
}
