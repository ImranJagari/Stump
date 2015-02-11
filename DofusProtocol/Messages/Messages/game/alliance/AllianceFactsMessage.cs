

// Generated on 02/11/2015 10:20:25
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class AllianceFactsMessage : Message
    {
        public const uint Id = 6414;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public Types.AllianceFactSheetInformations infos;
        public IEnumerable<Types.GuildInAllianceInformations> guilds;
        public IEnumerable<short> controlledSubareaIds;
        public int leaderCharacterId;
        public string leaderCharacterName;
        
        public AllianceFactsMessage()
        {
        }
        
        public AllianceFactsMessage(Types.AllianceFactSheetInformations infos, IEnumerable<Types.GuildInAllianceInformations> guilds, IEnumerable<short> controlledSubareaIds, int leaderCharacterId, string leaderCharacterName)
        {
            this.infos = infos;
            this.guilds = guilds;
            this.controlledSubareaIds = controlledSubareaIds;
            this.leaderCharacterId = leaderCharacterId;
            this.leaderCharacterName = leaderCharacterName;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(infos.TypeId);
            infos.Serialize(writer);
            var guilds_before = writer.Position;
            var guilds_count = 0;
            writer.WriteUShort(0);
            foreach (var entry in guilds)
            {
                 entry.Serialize(writer);
                 guilds_count++;
            }
            var guilds_after = writer.Position;
            writer.Seek((int)guilds_before);
            writer.WriteUShort((ushort)guilds_count);
            writer.Seek((int)guilds_after);

            var controlledSubareaIds_before = writer.Position;
            var controlledSubareaIds_count = 0;
            writer.WriteUShort(0);
            foreach (var entry in controlledSubareaIds)
            {
                 writer.WriteVarShort(entry);
                 controlledSubareaIds_count++;
            }
            var controlledSubareaIds_after = writer.Position;
            writer.Seek((int)controlledSubareaIds_before);
            writer.WriteUShort((ushort)controlledSubareaIds_count);
            writer.Seek((int)controlledSubareaIds_after);

            writer.WriteVarInt(leaderCharacterId);
            writer.WriteUTF(leaderCharacterName);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            infos = Types.ProtocolTypeManager.GetInstance<Types.AllianceFactSheetInformations>(reader.ReadShort());
            infos.Deserialize(reader);
            var limit = reader.ReadUShort();
            var guilds_ = new Types.GuildInAllianceInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 guilds_[i] = new Types.GuildInAllianceInformations();
                 guilds_[i].Deserialize(reader);
            }
            guilds = guilds_;
            limit = reader.ReadUShort();
            var controlledSubareaIds_ = new short[limit];
            for (int i = 0; i < limit; i++)
            {
                 controlledSubareaIds_[i] = reader.ReadVarShort();
            }
            controlledSubareaIds = controlledSubareaIds_;
            leaderCharacterId = reader.ReadVarInt();
            if (leaderCharacterId < 0)
                throw new Exception("Forbidden value on leaderCharacterId = " + leaderCharacterId + ", it doesn't respect the following condition : leaderCharacterId < 0");
            leaderCharacterName = reader.ReadUTF();
        }
        
    }
    
}