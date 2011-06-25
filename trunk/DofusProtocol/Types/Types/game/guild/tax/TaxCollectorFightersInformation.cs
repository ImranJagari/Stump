// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'TaxCollectorFightersInformation.xml' the '24/06/2011 12:04:59'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class TaxCollectorFightersInformation
	{
		public const uint Id = 169;
		public virtual short TypeId
		{
			get
			{
				return 169;
			}
		}
		
		public int collectorId;
		public Types.CharacterMinimalPlusLookInformations[] allyCharactersInformations;
		public Types.CharacterMinimalPlusLookInformations[] enemyCharactersInformations;
		
		public TaxCollectorFightersInformation()
		{
		}
		
		public TaxCollectorFightersInformation(int collectorId, Types.CharacterMinimalPlusLookInformations[] allyCharactersInformations, Types.CharacterMinimalPlusLookInformations[] enemyCharactersInformations)
		{
			this.collectorId = collectorId;
			this.allyCharactersInformations = allyCharactersInformations;
			this.enemyCharactersInformations = enemyCharactersInformations;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteInt(collectorId);
			writer.WriteUShort((ushort)allyCharactersInformations.Length);
			for (int i = 0; i < allyCharactersInformations.Length; i++)
			{
				allyCharactersInformations[i].Serialize(writer);
			}
			writer.WriteUShort((ushort)enemyCharactersInformations.Length);
			for (int i = 0; i < enemyCharactersInformations.Length; i++)
			{
				enemyCharactersInformations[i].Serialize(writer);
			}
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			collectorId = reader.ReadInt();
			int limit = reader.ReadUShort();
			allyCharactersInformations = new Types.CharacterMinimalPlusLookInformations[limit];
			for (int i = 0; i < limit; i++)
			{
				allyCharactersInformations[i] = new Types.CharacterMinimalPlusLookInformations();
				allyCharactersInformations[i].Deserialize(reader);
			}
			limit = reader.ReadUShort();
			enemyCharactersInformations = new Types.CharacterMinimalPlusLookInformations[limit];
			for (int i = 0; i < limit; i++)
			{
				enemyCharactersInformations[i] = new Types.CharacterMinimalPlusLookInformations();
				enemyCharactersInformations[i].Deserialize(reader);
			}
		}
	}
}
