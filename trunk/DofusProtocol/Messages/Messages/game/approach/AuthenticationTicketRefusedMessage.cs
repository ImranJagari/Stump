// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'AuthenticationTicketRefusedMessage.xml' the '05/11/2011 17:25:46'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class AuthenticationTicketRefusedMessage : Message
	{
		public const uint Id = 112;
		public override uint MessageId
		{
			get
			{
				return 112;
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
