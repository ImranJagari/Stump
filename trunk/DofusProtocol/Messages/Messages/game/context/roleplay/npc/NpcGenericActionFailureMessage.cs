// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'NpcGenericActionFailureMessage.xml' the '24/06/2011 12:04:50'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class NpcGenericActionFailureMessage : Message
	{
		public const uint Id = 5900;
		public override uint MessageId
		{
			get
			{
				return 5900;
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
