

// Generated on 03/02/2014 20:42:45
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class QuestListMessage : Message
    {
        public const uint Id = 5626;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public IEnumerable<short> finishedQuestsIds;
        public IEnumerable<short> finishedQuestsCounts;
        public IEnumerable<Types.QuestActiveInformations> activeQuests;
        
        public QuestListMessage()
        {
        }
        
        public QuestListMessage(IEnumerable<short> finishedQuestsIds, IEnumerable<short> finishedQuestsCounts, IEnumerable<Types.QuestActiveInformations> activeQuests)
        {
            this.finishedQuestsIds = finishedQuestsIds;
            this.finishedQuestsCounts = finishedQuestsCounts;
            this.activeQuests = activeQuests;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            var finishedQuestsIds_before = writer.Position;
            var finishedQuestsIds_count = 0;
            writer.WriteUShort(0);
            foreach (var entry in finishedQuestsIds)
            {
                 writer.WriteShort(entry);
                 finishedQuestsIds_count++;
            }
            var finishedQuestsIds_after = writer.Position;
            writer.Seek((int)finishedQuestsIds_before);
            writer.WriteUShort((ushort)finishedQuestsIds_count);
            writer.Seek((int)finishedQuestsIds_after);

            var finishedQuestsCounts_before = writer.Position;
            var finishedQuestsCounts_count = 0;
            writer.WriteUShort(0);
            foreach (var entry in finishedQuestsCounts)
            {
                 writer.WriteShort(entry);
                 finishedQuestsCounts_count++;
            }
            var finishedQuestsCounts_after = writer.Position;
            writer.Seek((int)finishedQuestsCounts_before);
            writer.WriteUShort((ushort)finishedQuestsCounts_count);
            writer.Seek((int)finishedQuestsCounts_after);

            var activeQuests_before = writer.Position;
            var activeQuests_count = 0;
            writer.WriteUShort(0);
            foreach (var entry in activeQuests)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
                 activeQuests_count++;
            }
            var activeQuests_after = writer.Position;
            writer.Seek((int)activeQuests_before);
            writer.WriteUShort((ushort)activeQuests_count);
            writer.Seek((int)activeQuests_after);

        }
        
        public override void Deserialize(IDataReader reader)
        {
            var limit = reader.ReadUShort();
            var finishedQuestsIds_ = new short[limit];
            for (int i = 0; i < limit; i++)
            {
                 finishedQuestsIds_[i] = reader.ReadShort();
            }
            finishedQuestsIds = finishedQuestsIds_;
            limit = reader.ReadUShort();
            var finishedQuestsCounts_ = new short[limit];
            for (int i = 0; i < limit; i++)
            {
                 finishedQuestsCounts_[i] = reader.ReadShort();
            }
            finishedQuestsCounts = finishedQuestsCounts_;
            limit = reader.ReadUShort();
            var activeQuests_ = new Types.QuestActiveInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 activeQuests_[i] = Types.ProtocolTypeManager.GetInstance<Types.QuestActiveInformations>(reader.ReadShort());
                 activeQuests_[i].Deserialize(reader);
            }
            activeQuests = activeQuests_;
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(short) + finishedQuestsIds.Sum(x => sizeof(short)) + sizeof(short) + finishedQuestsCounts.Sum(x => sizeof(short)) + sizeof(short) + activeQuests.Sum(x => sizeof(short) + x.GetSerializationSize());
        }
        
    }
    
}