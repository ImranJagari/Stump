// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'MapRunningFightListRequestMessage.xml' the '05/11/2011 17:25:50'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class MapRunningFightListRequestMessage : Message
	{
		public const uint Id = 5742;
		public override uint MessageId
		{
			get
			{
				return 5742;
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
