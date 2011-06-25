// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'FriendAddRequestMessage.xml' the '24/06/2011 12:04:52'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class FriendAddRequestMessage : Message
	{
		public const uint Id = 4004;
		public override uint MessageId
		{
			get
			{
				return 4004;
			}
		}
		
		public string name;
		
		public FriendAddRequestMessage()
		{
		}
		
		public FriendAddRequestMessage(string name)
		{
			this.name = name;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUTF(name);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			name = reader.ReadUTF();
		}
	}
}
