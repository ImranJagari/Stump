// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameContextCreateRequestMessage.xml' the '24/06/2011 12:04:48'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameContextCreateRequestMessage : Message
	{
		public const uint Id = 250;
		public override uint MessageId
		{
			get
			{
				return 250;
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
