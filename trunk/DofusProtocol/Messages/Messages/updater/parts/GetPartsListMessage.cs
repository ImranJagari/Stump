// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GetPartsListMessage.xml' the '05/11/2011 17:26:00'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GetPartsListMessage : Message
	{
		public const uint Id = 1501;
		public override uint MessageId
		{
			get
			{
				return 1501;
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
