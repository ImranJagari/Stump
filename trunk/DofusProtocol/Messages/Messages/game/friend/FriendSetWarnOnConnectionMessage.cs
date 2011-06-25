// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'FriendSetWarnOnConnectionMessage.xml' the '24/06/2011 12:04:52'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class FriendSetWarnOnConnectionMessage : Message
	{
		public const uint Id = 5602;
		public override uint MessageId
		{
			get
			{
				return 5602;
			}
		}
		
		public bool enable;
		
		public FriendSetWarnOnConnectionMessage()
		{
		}
		
		public FriendSetWarnOnConnectionMessage(bool enable)
		{
			this.enable = enable;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteBoolean(enable);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			enable = reader.ReadBoolean();
		}
	}
}
