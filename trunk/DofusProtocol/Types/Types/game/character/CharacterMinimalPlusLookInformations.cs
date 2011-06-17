// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharacterMinimalPlusLookInformations.xml' the '14/06/2011 11:32:45'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class CharacterMinimalPlusLookInformations : CharacterMinimalInformations
	{
		public const uint Id = 163;
		public short TypeId
		{
			get
			{
				return 163;
			}
		}
		
		public Types.EntityLook entityLook;
		
		public CharacterMinimalPlusLookInformations()
		{
		}
		
		public CharacterMinimalPlusLookInformations(int id, byte level, string name, Types.EntityLook entityLook)
			 : base(id, level, name)
		{
			this.entityLook = entityLook;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			entityLook.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			entityLook = new Types.EntityLook();
			entityLook.Deserialize(reader);
		}
	}
}