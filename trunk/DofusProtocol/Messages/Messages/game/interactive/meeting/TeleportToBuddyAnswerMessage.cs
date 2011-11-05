// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'TeleportToBuddyAnswerMessage.xml' the '05/11/2011 17:25:56'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class TeleportToBuddyAnswerMessage : Message
	{
		public const uint Id = 6293;
		public override uint MessageId
		{
			get
			{
				return 6293;
			}
		}
		
		public short dungeonId;
		public int buddyId;
		public bool accept;
		
		public TeleportToBuddyAnswerMessage()
		{
		}
		
		public TeleportToBuddyAnswerMessage(short dungeonId, int buddyId, bool accept)
		{
			this.dungeonId = dungeonId;
			this.buddyId = buddyId;
			this.accept = accept;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(dungeonId);
			writer.WriteInt(buddyId);
			writer.WriteBoolean(accept);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			dungeonId = reader.ReadShort();
			if ( dungeonId < 0 )
			{
				throw new Exception("Forbidden value on dungeonId = " + dungeonId + ", it doesn't respect the following condition : dungeonId < 0");
			}
			buddyId = reader.ReadInt();
			if ( buddyId < 0 )
			{
				throw new Exception("Forbidden value on buddyId = " + buddyId + ", it doesn't respect the following condition : buddyId < 0");
			}
			accept = reader.ReadBoolean();
		}
	}
}
