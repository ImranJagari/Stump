// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'NicknameChoiceRequestMessage.xml' the '24/06/2011 12:04:45'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class NicknameChoiceRequestMessage : Message
	{
		public const uint Id = 5639;
		public override uint MessageId
		{
			get
			{
				return 5639;
			}
		}
		
		public string nickname;
		
		public NicknameChoiceRequestMessage()
		{
		}
		
		public NicknameChoiceRequestMessage(string nickname)
		{
			this.nickname = nickname;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUTF(nickname);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			nickname = reader.ReadUTF();
		}
	}
}
