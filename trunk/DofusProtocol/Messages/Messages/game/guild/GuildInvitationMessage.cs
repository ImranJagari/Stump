// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GuildInvitationMessage.xml' the '24/06/2011 12:04:52'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GuildInvitationMessage : Message
	{
		public const uint Id = 5551;
		public override uint MessageId
		{
			get
			{
				return 5551;
			}
		}
		
		public int targetId;
		
		public GuildInvitationMessage()
		{
		}
		
		public GuildInvitationMessage(int targetId)
		{
			this.targetId = targetId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(targetId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			targetId = reader.ReadInt();
			if ( targetId < 0 )
			{
				throw new Exception("Forbidden value on targetId = " + targetId + ", it doesn't respect the following condition : targetId < 0");
			}
		}
	}
}
