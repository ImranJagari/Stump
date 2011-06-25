// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ObjectErrorMessage.xml' the '24/06/2011 12:04:55'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ObjectErrorMessage : Message
	{
		public const uint Id = 3004;
		public override uint MessageId
		{
			get
			{
				return 3004;
			}
		}
		
		public byte reason;
		
		public ObjectErrorMessage()
		{
		}
		
		public ObjectErrorMessage(byte reason)
		{
			this.reason = reason;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteByte(reason);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			reason = reader.ReadByte();
		}
	}
}
