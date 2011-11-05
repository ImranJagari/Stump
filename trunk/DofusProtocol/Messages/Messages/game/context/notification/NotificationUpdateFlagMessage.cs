// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'NotificationUpdateFlagMessage.xml' the '05/11/2011 17:25:49'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class NotificationUpdateFlagMessage : Message
	{
		public const uint Id = 6090;
		public override uint MessageId
		{
			get
			{
				return 6090;
			}
		}
		
		public short index;
		
		public NotificationUpdateFlagMessage()
		{
		}
		
		public NotificationUpdateFlagMessage(short index)
		{
			this.index = index;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(index);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			index = reader.ReadShort();
			if ( index < 0 )
			{
				throw new Exception("Forbidden value on index = " + index + ", it doesn't respect the following condition : index < 0");
			}
		}
	}
}
