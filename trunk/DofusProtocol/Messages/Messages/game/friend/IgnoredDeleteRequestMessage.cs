// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'IgnoredDeleteRequestMessage.xml' the '05/11/2011 17:25:54'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class IgnoredDeleteRequestMessage : Message
	{
		public const uint Id = 5680;
		public override uint MessageId
		{
			get
			{
				return 5680;
			}
		}
		
		public string name;
		public bool session;
		
		public IgnoredDeleteRequestMessage()
		{
		}
		
		public IgnoredDeleteRequestMessage(string name, bool session)
		{
			this.name = name;
			this.session = session;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUTF(name);
			writer.WriteBoolean(session);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			name = reader.ReadUTF();
			session = reader.ReadBoolean();
		}
	}
}
