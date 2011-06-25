// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'AchievementFinishedMessage.xml' the '24/06/2011 12:04:45'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class AchievementFinishedMessage : Message
	{
		public const uint Id = 6208;
		public override uint MessageId
		{
			get
			{
				return 6208;
			}
		}
		
		public short achievementId;
		
		public AchievementFinishedMessage()
		{
		}
		
		public AchievementFinishedMessage(short achievementId)
		{
			this.achievementId = achievementId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(achievementId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			achievementId = reader.ReadShort();
			if ( achievementId < 0 )
			{
				throw new Exception("Forbidden value on achievementId = " + achievementId + ", it doesn't respect the following condition : achievementId < 0");
			}
		}
	}
}
