

// Generated on 12/20/2015 16:36:46
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class CharacterReplayRequestMessage : Message
    {
        public const uint Id = 167;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public long characterId;
        
        public CharacterReplayRequestMessage()
        {
        }
        
        public CharacterReplayRequestMessage(long characterId)
        {
            this.characterId = characterId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarLong(characterId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            characterId = reader.ReadVarLong();
            if (characterId < 0 || characterId > 9.007199254740992E15)
                throw new Exception("Forbidden value on characterId = " + characterId + ", it doesn't respect the following condition : characterId < 0 || characterId > 9.007199254740992E15");
        }
        
    }
    
}