// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'AcquaintanceSearchMessage.xml' the '24/06/2011 12:04:45'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class AcquaintanceSearchMessage : Message
	{
		public const uint Id = 6144;
		public override uint MessageId
		{
			get
			{
				return 6144;
			}
		}
		
		public string nickname;
		
		public AcquaintanceSearchMessage()
		{
		}
		
		public AcquaintanceSearchMessage(string nickname)
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
