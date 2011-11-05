// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'LockableChangeCodeMessage.xml' the '05/11/2011 17:25:51'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class LockableChangeCodeMessage : Message
	{
		public const uint Id = 5666;
		public override uint MessageId
		{
			get
			{
				return 5666;
			}
		}
		
		public string code;
		
		public LockableChangeCodeMessage()
		{
		}
		
		public LockableChangeCodeMessage(string code)
		{
			this.code = code;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUTF(code);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			code = reader.ReadUTF();
		}
	}
}
