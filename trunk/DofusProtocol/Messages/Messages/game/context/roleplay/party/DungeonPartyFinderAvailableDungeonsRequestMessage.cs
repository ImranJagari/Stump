// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'DungeonPartyFinderAvailableDungeonsRequestMessage.xml' the '30/06/2011 11:40:14'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class DungeonPartyFinderAvailableDungeonsRequestMessage : Message
	{
		public const uint Id = 6240;
		public override uint MessageId
		{
			get
			{
				return 6240;
			}
		}
		
		
		public override void Serialize(IDataWriter writer)
		{
		}
		
		public override void Deserialize(IDataReader reader)
		{
		}
	}
}