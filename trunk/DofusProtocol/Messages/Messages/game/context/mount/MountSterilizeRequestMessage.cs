// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'MountSterilizeRequestMessage.xml' the '05/11/2011 17:25:49'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class MountSterilizeRequestMessage : Message
	{
		public const uint Id = 5962;
		public override uint MessageId
		{
			get
			{
				return 5962;
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
