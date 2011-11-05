// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'FriendUpdateMessage.xml' the '05/11/2011 17:25:54'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class FriendUpdateMessage : Message
	{
		public const uint Id = 5924;
		public override uint MessageId
		{
			get
			{
				return 5924;
			}
		}
		
		public Types.FriendInformations friendUpdated;
		
		public FriendUpdateMessage()
		{
		}
		
		public FriendUpdateMessage(Types.FriendInformations friendUpdated)
		{
			this.friendUpdated = friendUpdated;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(friendUpdated.TypeId);
			friendUpdated.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			friendUpdated = Types.ProtocolTypeManager.GetInstance<Types.FriendInformations>(reader.ReadShort());
			friendUpdated.Deserialize(reader);
		}
	}
}
