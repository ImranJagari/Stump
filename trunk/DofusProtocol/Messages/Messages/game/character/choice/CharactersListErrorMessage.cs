// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharactersListErrorMessage.xml' the '05/11/2011 17:25:46'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class CharactersListErrorMessage : Message
	{
		public const uint Id = 5545;
		public override uint MessageId
		{
			get
			{
				return 5545;
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
