// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionNoopMessage.xml' the '05/11/2011 17:25:44'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameActionNoopMessage : Message
	{
		public const uint Id = 1002;
		public override uint MessageId
		{
			get
			{
				return 1002;
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
