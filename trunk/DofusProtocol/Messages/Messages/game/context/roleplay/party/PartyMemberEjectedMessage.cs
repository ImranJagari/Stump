// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PartyMemberEjectedMessage.xml' the '05/11/2011 17:25:53'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PartyMemberEjectedMessage : PartyMemberRemoveMessage
	{
		public const uint Id = 6252;
		public override uint MessageId
		{
			get
			{
				return 6252;
			}
		}
		
		public int kickerId;
		
		public PartyMemberEjectedMessage()
		{
		}
		
		public PartyMemberEjectedMessage(int partyId, int leavingPlayerId, int kickerId)
			 : base(partyId, leavingPlayerId)
		{
			this.kickerId = kickerId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(kickerId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			kickerId = reader.ReadInt();
			if ( kickerId < 0 )
			{
				throw new Exception("Forbidden value on kickerId = " + kickerId + ", it doesn't respect the following condition : kickerId < 0");
			}
		}
	}
}
