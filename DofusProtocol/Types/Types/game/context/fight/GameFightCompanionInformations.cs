

// Generated on 10/30/2016 16:20:54
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class GameFightCompanionInformations : GameFightFighterInformations
    {
        public const short Id = 450;
        public override short TypeId
        {
            get { return Id; }
        }
        
        public sbyte companionGenericId;
        public byte level;
        public double masterId;
        
        public GameFightCompanionInformations()
        {
        }
        
        public GameFightCompanionInformations(double contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, sbyte teamId, sbyte wave, bool alive, Types.GameFightMinimalStats stats, IEnumerable<short> previousPositions, sbyte companionGenericId, byte level, double masterId)
         : base(contextualId, look, disposition, teamId, wave, alive, stats, previousPositions)
        {
            this.companionGenericId = companionGenericId;
            this.level = level;
            this.masterId = masterId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(companionGenericId);
            writer.WriteByte(level);
            writer.WriteDouble(masterId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            companionGenericId = reader.ReadSByte();
            if (companionGenericId < 0)
                throw new Exception("Forbidden value on companionGenericId = " + companionGenericId + ", it doesn't respect the following condition : companionGenericId < 0");
            level = reader.ReadByte();
            if (level < 0 || level > 255)
                throw new Exception("Forbidden value on level = " + level + ", it doesn't respect the following condition : level < 0 || level > 255");
            masterId = reader.ReadDouble();
            if (masterId < -9007199254740990 || masterId > 9007199254740990)
                throw new Exception("Forbidden value on masterId = " + masterId + ", it doesn't respect the following condition : masterId < -9007199254740990 || masterId > 9007199254740990");
        }
        
        
    }
    
}