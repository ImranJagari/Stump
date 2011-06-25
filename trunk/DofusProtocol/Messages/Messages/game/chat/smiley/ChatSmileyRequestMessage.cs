// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ChatSmileyRequestMessage.xml' the '24/06/2011 12:04:48'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ChatSmileyRequestMessage : Message
	{
		public const uint Id = 800;
		public override uint MessageId
		{
			get
			{
				return 800;
			}
		}
		
		public byte smileyId;
		
		public ChatSmileyRequestMessage()
		{
		}
		
		public ChatSmileyRequestMessage(byte smileyId)
		{
			this.smileyId = smileyId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteByte(smileyId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			smileyId = reader.ReadByte();
			if ( smileyId < 0 )
			{
				throw new Exception("Forbidden value on smileyId = " + smileyId + ", it doesn't respect the following condition : smileyId < 0");
			}
		}
	}
}
