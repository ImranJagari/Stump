// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameContextDestroyMessage.xml' the '05/11/2011 17:25:47'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameContextDestroyMessage : Message
	{
		public const uint Id = 201;
		public override uint MessageId
		{
			get
			{
				return 201;
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
