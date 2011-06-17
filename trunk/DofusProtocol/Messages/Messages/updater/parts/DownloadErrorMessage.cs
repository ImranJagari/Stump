// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'DownloadErrorMessage.xml' the '15/06/2011 01:39:10'
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
		
		public byte errorId;
		
		public DownloadErrorMessage()
		{
		}
		
		public DownloadErrorMessage(byte errorId)
		{
			this.errorId = errorId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteByte(errorId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			errorId = reader.ReadByte();
			if ( errorId < 0 )
			{
				throw new Exception("Forbidden value on errorId = " + errorId + ", it doesn't respect the following condition : errorId < 0");
			}
		}
	}
}