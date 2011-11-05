// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'QuestStepStartedMessage.xml' the '05/11/2011 17:25:53'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class QuestStepStartedMessage : Message
	{
		public const uint Id = 6096;
		public override uint MessageId
		{
			get
			{
				return 6096;
			}
		}
		
		public ushort questId;
		public ushort stepId;
		
		public QuestStepStartedMessage()
		{
		}
		
		public QuestStepStartedMessage(ushort questId, ushort stepId)
		{
			this.questId = questId;
			this.stepId = stepId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort(questId);
			writer.WriteUShort(stepId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			questId = reader.ReadUShort();
			if ( questId < 0 || questId > 65535 )
			{
				throw new Exception("Forbidden value on questId = " + questId + ", it doesn't respect the following condition : questId < 0 || questId > 65535");
			}
			stepId = reader.ReadUShort();
			if ( stepId < 0 || stepId > 65535 )
			{
				throw new Exception("Forbidden value on stepId = " + stepId + ", it doesn't respect the following condition : stepId < 0 || stepId > 65535");
			}
		}
	}
}
