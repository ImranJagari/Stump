// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ShortcutSpell.xml' the '05/11/2011 17:26:03'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class ShortcutSpell : Shortcut
	{
		public const uint Id = 368;
		public override short TypeId
		{
			get
			{
				return 368;
			}
		}
		
		public short spellId;
		
		public ShortcutSpell()
		{
		}
		
		public ShortcutSpell(int slot, short spellId)
			 : base(slot)
		{
			this.spellId = spellId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteShort(spellId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			spellId = reader.ReadShort();
			if ( spellId < 0 )
			{
				throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
			}
		}
	}
}
