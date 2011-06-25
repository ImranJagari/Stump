// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'FriendSpouseFollowWithCompassRequestMessage.xml' the '24/06/2011 12:04:52'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class FriendSpouseFollowWithCompassRequestMessage : Message
	{
		public const uint Id = 5606;
		public override uint MessageId
		{
			get
			{
				return 5606;
			}
		}
		
		public bool enable;
		
		public FriendSpouseFollowWithCompassRequestMessage()
		{
		}
		
		public FriendSpouseFollowWithCompassRequestMessage(bool enable)
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
