// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'EmotePlayErrorMessage.xml' the '24/06/2011 12:04:49'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class EmotePlayErrorMessage : Message
	{
		public const uint Id = 5688;
		public override uint MessageId
		{
			get
			{
				return 5688;
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
