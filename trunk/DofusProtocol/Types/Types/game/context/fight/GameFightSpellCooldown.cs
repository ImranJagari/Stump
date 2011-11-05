// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameFightSpellCooldown.xml' the '05/11/2011 17:26:01'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class GameFightSpellCooldown
	{
		public const uint Id = 205;
		public virtual short TypeId
		{
			get
			{
				return 205;
			}
		}
		
		public int spellId;
		public sbyte cooldown;
		
		public GameFightSpellCooldown()
		{
		}
		
		public GameFightSpellCooldown(int spellId, sbyte cooldown)
		{
			this.spellId = spellId;
			this.cooldown = cooldown;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteInt(spellId);
			writer.WriteSByte(cooldown);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			spellId = reader.ReadInt();
			cooldown = reader.ReadSByte();
			if ( cooldown < 0 )
			{
				throw new Exception("Forbidden value on cooldown = " + cooldown + ", it doesn't respect the following condition : cooldown < 0");
			}
		}
	}
}
