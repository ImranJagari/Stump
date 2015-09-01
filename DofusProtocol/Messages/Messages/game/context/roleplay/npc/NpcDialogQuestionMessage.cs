

// Generated on 09/01/2015 10:48:12
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class NpcDialogQuestionMessage : Message
    {
        public const uint Id = 5617;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public short messageId;
        public IEnumerable<string> dialogParams;
        public IEnumerable<short> visibleReplies;
        
        public NpcDialogQuestionMessage()
        {
        }
        
        public NpcDialogQuestionMessage(short messageId, IEnumerable<string> dialogParams, IEnumerable<short> visibleReplies)
        {
            this.messageId = messageId;
            this.dialogParams = dialogParams;
            this.visibleReplies = visibleReplies;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(messageId);
            var dialogParams_before = writer.Position;
            var dialogParams_count = 0;
            writer.WriteUShort(0);
            foreach (var entry in dialogParams)
            {
                 writer.WriteUTF(entry);
                 dialogParams_count++;
            }
            var dialogParams_after = writer.Position;
            writer.Seek((int)dialogParams_before);
            writer.WriteUShort((ushort)dialogParams_count);
            writer.Seek((int)dialogParams_after);

            var visibleReplies_before = writer.Position;
            var visibleReplies_count = 0;
            writer.WriteUShort(0);
            foreach (var entry in visibleReplies)
            {
                 writer.WriteVarShort(entry);
                 visibleReplies_count++;
            }
            var visibleReplies_after = writer.Position;
            writer.Seek((int)visibleReplies_before);
            writer.WriteUShort((ushort)visibleReplies_count);
            writer.Seek((int)visibleReplies_after);

        }
        
        public override void Deserialize(IDataReader reader)
        {
            messageId = reader.ReadVarShort();
            if (messageId < 0)
                throw new Exception("Forbidden value on messageId = " + messageId + ", it doesn't respect the following condition : messageId < 0");
            var limit = reader.ReadUShort();
            var dialogParams_ = new string[limit];
            for (int i = 0; i < limit; i++)
            {
                 dialogParams_[i] = reader.ReadUTF();
            }
            dialogParams = dialogParams_;
            limit = reader.ReadUShort();
            var visibleReplies_ = new short[limit];
            for (int i = 0; i < limit; i++)
            {
                 visibleReplies_[i] = reader.ReadVarShort();
            }
            visibleReplies = visibleReplies_;
        }
        
    }
    
}