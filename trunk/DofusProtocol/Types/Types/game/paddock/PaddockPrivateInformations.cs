// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PaddockPrivateInformations.xml' the '14/06/2011 11:32:50'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class PaddockPrivateInformations : PaddockAbandonnedInformations
	{
		public const uint Id = 131;
		public short TypeId
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
		
		public PaddockPrivateInformations(short maxOutdoorMount, short maxItems, int price, int guildId, Types.GuildInformations guildInfo)
			 : base(maxOutdoorMount, maxItems, price, guildId)
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