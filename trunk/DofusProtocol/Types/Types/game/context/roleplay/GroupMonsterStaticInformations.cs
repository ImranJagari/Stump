// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GroupMonsterStaticInformations.xml' the '24/06/2011 12:04:58'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class GroupMonsterStaticInformations
	{
		public const uint Id = 140;
		public virtual short TypeId
		{
			get
			{
				return 140;
			}
		}
		
		public int mainCreatureGenericId;
		public byte mainCreatureGrade;
		public Types.MonsterInGroupInformations[] underlings;
		
		public GroupMonsterStaticInformations()
		{
		}
		
		public GroupMonsterStaticInformations(int mainCreatureGenericId, byte mainCreatureGrade, Types.MonsterInGroupInformations[] underlings)
		{
			this.mainCreatureGenericId = mainCreatureGenericId;
			this.mainCreatureGrade = mainCreatureGrade;
			this.underlings = underlings;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteInt(mainCreatureGenericId);
			writer.WriteByte(mainCreatureGrade);
			writer.WriteUShort((ushort)underlings.Length);
			for (int i = 0; i < underlings.Length; i++)
			{
				underlings[i].Serialize(writer);
			}
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			mainCreatureGenericId = reader.ReadInt();
			mainCreatureGrade = reader.ReadByte();
			if ( mainCreatureGrade < 0 )
			{
				throw new Exception("Forbidden value on mainCreatureGrade = " + mainCreatureGrade + ", it doesn't respect the following condition : mainCreatureGrade < 0");
			}
			int limit = reader.ReadUShort();
			underlings = new Types.MonsterInGroupInformations[limit];
			for (int i = 0; i < limit; i++)
			{
				underlings[i] = new Types.MonsterInGroupInformations();
				underlings[i].Deserialize(reader);
			}
		}
	}
}
