// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PrismCurrentBonusRequestMessage.xml' the '05/11/2011 17:25:59'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PrismCurrentBonusRequestMessage : Message
	{
		public const uint Id = 5840;
		public override uint MessageId
		{
			get
			{
				return 5840;
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
