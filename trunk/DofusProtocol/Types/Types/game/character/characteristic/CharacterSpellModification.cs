// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharacterSpellModification.xml' the '14/06/2011 11:32:45'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class CharacterSpellModification
	{
		public const uint Id = 215;
		public short TypeId
		{
			get
			{
				return 215;
			}
		}
		
		public byte modificationType;
		public short spellId;
		public Types.CharacterBaseCharacteristic value;
		
		public CharacterSpellModification()
		{
		}
		
		public CharacterSpellModification(byte modificationType, short spellId, Types.CharacterBaseCharacteristic value)
		{
			this.modificationType = modificationType;
			this.spellId = spellId;
			this.value = value;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteByte(modificationType);
			writer.WriteShort(spellId);
			value.Serialize(writer);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			modificationType = reader.ReadByte();
			if ( modificationType < 0 )
			{
				throw new Exception("Forbidden value on modificationType = " + modificationType + ", it doesn't respect the following condition : modificationType < 0");
			}
			spellId = reader.ReadShort();
			if ( spellId < 0 )
			{
				throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
			}
			value = new Types.CharacterBaseCharacteristic();
			value.Deserialize(reader);
		}
	}
}