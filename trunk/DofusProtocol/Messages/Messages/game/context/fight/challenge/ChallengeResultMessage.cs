// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ChallengeResultMessage.xml' the '05/11/2011 17:25:49'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ChallengeResultMessage : Message
	{
		public const uint Id = 6019;
		public override uint MessageId
		{
			get
			{
				return 6019;
			}
		}
		
		public sbyte challengeId;
		public bool success;
		
		public ChallengeResultMessage()
		{
		}
		
		public ChallengeResultMessage(sbyte challengeId, bool success)
		{
			this.challengeId = challengeId;
			this.success = success;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(challengeId);
			writer.WriteBoolean(success);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			challengeId = reader.ReadSByte();
			if ( challengeId < 0 )
			{
				throw new Exception("Forbidden value on challengeId = " + challengeId + ", it doesn't respect the following condition : challengeId < 0");
			}
			success = reader.ReadBoolean();
		}
	}
}
