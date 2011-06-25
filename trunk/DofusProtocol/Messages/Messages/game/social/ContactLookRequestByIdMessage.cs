// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ContactLookRequestByIdMessage.xml' the '24/06/2011 12:04:57'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ContactLookRequestByIdMessage : ContactLookRequestMessage
	{
		public const uint Id = 5935;
		public override uint MessageId
		{
			get
			{
				return 5935;
			}
		}
		
		public int playerId;
		
		public ContactLookRequestByIdMessage()
		{
		}
		
		public ContactLookRequestByIdMessage(byte requestId, byte contactType, int playerId)
			 : base(requestId, contactType)
		{
			this.playerId = playerId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(playerId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			playerId = reader.ReadInt();
			if ( playerId < 0 )
			{
				throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0");
			}
		}
	}
}
