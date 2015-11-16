

// Generated on 11/16/2015 14:25:55
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class AchievementFinishedInformationMessage : AchievementFinishedMessage
    {
        public const uint Id = 6381;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public string name;
        public int playerId;
        
        public AchievementFinishedInformationMessage()
        {
        }
        
        public AchievementFinishedInformationMessage(short id, byte finishedlevel, string name, int playerId)
         : base(id, finishedlevel)
        {
            this.name = name;
            this.playerId = playerId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(name);
            writer.WriteVarInt(playerId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            name = reader.ReadUTF();
            playerId = reader.ReadVarInt();
            if (playerId < 0)
                throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0");
        }
        
    }
    
}