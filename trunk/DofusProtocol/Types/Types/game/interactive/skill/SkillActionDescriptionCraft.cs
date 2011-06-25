// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'SkillActionDescriptionCraft.xml' the '24/06/2011 12:04:59'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class SkillActionDescriptionCraft : SkillActionDescription
	{
		public const uint Id = 100;
		public override short TypeId
		{
			get
			{
				return 100;
			}
		}
		
		public byte maxSlots;
		public byte probability;
		
		public SkillActionDescriptionCraft()
		{
		}
		
		public SkillActionDescriptionCraft(short skillId, byte maxSlots, byte probability)
			 : base(skillId)
		{
			this.maxSlots = maxSlots;
			this.probability = probability;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteByte(maxSlots);
			writer.WriteByte(probability);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			maxSlots = reader.ReadByte();
			if ( maxSlots < 0 )
			{
				throw new Exception("Forbidden value on maxSlots = " + maxSlots + ", it doesn't respect the following condition : maxSlots < 0");
			}
			probability = reader.ReadByte();
			if ( probability < 0 )
			{
				throw new Exception("Forbidden value on probability = " + probability + ", it doesn't respect the following condition : probability < 0");
			}
		}
	}
}
