

// Generated on 10/28/2014 16:38:00
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class CharacterHardcoreOrEpicInformations : CharacterBaseInformations
    {
        public const short Id = 474;
        public override short TypeId
        {
            get { return Id; }
        }
        
        public sbyte deathState;
        public short deathCount;
        public byte deathMaxLevel;
        
        public CharacterHardcoreOrEpicInformations()
        {
        }
        
        public CharacterHardcoreOrEpicInformations(int id, byte level, string name, Types.EntityLook entityLook, sbyte breed, bool sex, sbyte deathState, short deathCount, byte deathMaxLevel)
         : base(id, level, name, entityLook, breed, sex)
        {
            this.deathState = deathState;
            this.deathCount = deathCount;
            this.deathMaxLevel = deathMaxLevel;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(deathState);
            writer.WriteShort(deathCount);
            writer.WriteByte(deathMaxLevel);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            deathState = reader.ReadSByte();
            if (deathState < 0)
                throw new Exception("Forbidden value on deathState = " + deathState + ", it doesn't respect the following condition : deathState < 0");
            deathCount = reader.ReadShort();
            if (deathCount < 0)
                throw new Exception("Forbidden value on deathCount = " + deathCount + ", it doesn't respect the following condition : deathCount < 0");
            deathMaxLevel = reader.ReadByte();
            if (deathMaxLevel < 1 || deathMaxLevel > 200)
                throw new Exception("Forbidden value on deathMaxLevel = " + deathMaxLevel + ", it doesn't respect the following condition : deathMaxLevel < 1 || deathMaxLevel > 200");
        }
        
        public override int GetSerializationSize()
        {
            return base.GetSerializationSize() + sizeof(sbyte) + sizeof(short) + sizeof(byte);
        }
        
    }
    
}