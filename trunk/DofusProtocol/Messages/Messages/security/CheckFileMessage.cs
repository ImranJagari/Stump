// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CheckFileMessage.xml' the '24/06/2011 12:04:57'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class CheckFileMessage : Message
	{
		public const uint Id = 6156;
		public override uint MessageId
		{
			get
			{
				return 6156;
			}
		}
		
		public string filenameHash;
		public byte type;
		public string value;
		
		public CheckFileMessage()
		{
		}
		
		public CheckFileMessage(string filenameHash, byte type, string value)
		{
			this.filenameHash = filenameHash;
			this.type = type;
			this.value = value;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUTF(filenameHash);
			writer.WriteByte(type);
			writer.WriteUTF(value);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			filenameHash = reader.ReadUTF();
			type = reader.ReadByte();
			if ( type < 0 )
			{
				throw new Exception("Forbidden value on type = " + type + ", it doesn't respect the following condition : type < 0");
			}
			value = reader.ReadUTF();
		}
	}
}
