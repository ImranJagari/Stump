// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameContextQuitMessage.xml' the '05/11/2011 17:25:47'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameContextQuitMessage : Message
	{
		public const uint Id = 255;
		public override uint MessageId
		{
			get
			{
				return 255;
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
