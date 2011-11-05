// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'DownloadErrorMessage.xml' the '05/11/2011 17:26:00'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class DownloadErrorMessage : Message
	{
		public const uint Id = 1513;
		public override uint MessageId
		{
			get
			{
				return 1513;
			}
		}
		
		public sbyte errorId;
		public string message;
		public string helpUrl;
		
		public DownloadErrorMessage()
		{
		}
		
		public DownloadErrorMessage(sbyte errorId, string message, string helpUrl)
		{
			this.errorId = errorId;
			this.message = message;
			this.helpUrl = helpUrl;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(errorId);
			writer.WriteUTF(message);
			writer.WriteUTF(helpUrl);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			errorId = reader.ReadSByte();
			if ( errorId < 0 )
			{
				throw new Exception("Forbidden value on errorId = " + errorId + ", it doesn't respect the following condition : errorId < 0");
			}
			message = reader.ReadUTF();
			helpUrl = reader.ReadUTF();
		}
	}
}
