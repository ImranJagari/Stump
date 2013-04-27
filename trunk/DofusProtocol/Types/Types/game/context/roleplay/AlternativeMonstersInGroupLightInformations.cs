
// Generated on 03/25/2013 19:24:29
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class AlternativeMonstersInGroupLightInformations
    {
        public const short Id = 394;
        public virtual short TypeId
        {
            get { return Id; }
        }
        
        public int playerCount;
        public IEnumerable<Types.MonsterInGroupLightInformations> monsters;
        
        public AlternativeMonstersInGroupLightInformations()
        {
        }
        
        public AlternativeMonstersInGroupLightInformations(int playerCount, IEnumerable<Types.MonsterInGroupLightInformations> monsters)
        {
            this.playerCount = playerCount;
            this.monsters = monsters;
        }
        
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt(playerCount);
            writer.WriteUShort((ushort)monsters.Count());
            foreach (var entry in monsters)
            {
                 entry.Serialize(writer);
            }
        }
        
        public virtual void Deserialize(IDataReader reader)
        {
            playerCount = reader.ReadInt();
            var limit = reader.ReadUShort();
            monsters = new Types.MonsterInGroupLightInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 (monsters as Types.MonsterInGroupLightInformations[])[i] = new Types.MonsterInGroupLightInformations();
                 (monsters as Types.MonsterInGroupLightInformations[])[i].Deserialize(reader);
            }
        }
        
    }
    
}