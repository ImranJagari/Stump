// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ChallengeTargetUpdateMessage.xml' the '05/11/2011 17:25:49'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ChallengeTargetUpdateMessage : Message
	{
		public const uint Id = 6123;
		public override uint MessageId
		{
			get
			{
				return 6123;
			}
		}
		
		public sbyte challengeId;
		public int targetId;
		
		public ChallengeTargetUpdateMessage()
		{
		}
		
		public ChallengeTargetUpdateMessage(sbyte challengeId, int targetId)
		{
			this.challengeId = challengeId;
			this.targetId = targetId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(challengeId);
			writer.WriteInt(targetId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			challengeId = reader.ReadSByte();
			if ( challengeId < 0 )
			{
				throw new Exception("Forbidden value on challengeId = " + challengeId + ", it doesn't respect the following condition : challengeId < 0");
			}
			targetId = reader.ReadInt();
		}
	}
}
