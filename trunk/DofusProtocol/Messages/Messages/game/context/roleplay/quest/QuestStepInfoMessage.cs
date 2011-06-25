// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'QuestStepInfoMessage.xml' the '24/06/2011 12:04:51'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class QuestStepInfoMessage : Message
	{
		public const uint Id = 5625;
		public override uint MessageId
		{
			get
			{
				return 5625;
			}
		}
		
		public short questId;
		public short stepId;
		public short[] objectivesIds;
		public bool[] objectivesStatus;
		
		public QuestStepInfoMessage()
		{
		}
		
		public QuestStepInfoMessage(short questId, short stepId, short[] objectivesIds, bool[] objectivesStatus)
		{
			this.questId = questId;
			this.stepId = stepId;
			this.objectivesIds = objectivesIds;
			this.objectivesStatus = objectivesStatus;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(questId);
			writer.WriteShort(stepId);
			writer.WriteUShort((ushort)objectivesIds.Length);
			for (int i = 0; i < objectivesIds.Length; i++)
			{
				writer.WriteShort(objectivesIds[i]);
			}
			writer.WriteUShort((ushort)objectivesStatus.Length);
			for (int i = 0; i < objectivesStatus.Length; i++)
			{
				writer.WriteBoolean(objectivesStatus[i]);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			questId = reader.ReadShort();
			if ( questId < 0 )
			{
				throw new Exception("Forbidden value on questId = " + questId + ", it doesn't respect the following condition : questId < 0");
			}
			stepId = reader.ReadShort();
			if ( stepId < 0 )
			{
				throw new Exception("Forbidden value on stepId = " + stepId + ", it doesn't respect the following condition : stepId < 0");
			}
			int limit = reader.ReadUShort();
			objectivesIds = new short[limit];
			for (int i = 0; i < limit; i++)
			{
				objectivesIds[i] = reader.ReadShort();
			}
			limit = reader.ReadUShort();
			objectivesStatus = new bool[limit];
			for (int i = 0; i < limit; i++)
			{
				objectivesStatus[i] = reader.ReadBoolean();
			}
		}
	}
}
