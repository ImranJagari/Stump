// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameFightSpellCooldown.xml' the '24/06/2011 12:04:58'
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
		public byte cooldown;
		
		public GameFightSpellCooldown()
		{
		}
		
		public GameFightSpellCooldown(int spellId, byte cooldown)
		{
			this.spellId = spellId;
			this.cooldown = cooldown;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteInt(spellId);
			writer.WriteByte(cooldown);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			spellId = reader.ReadInt();
			cooldown = reader.ReadByte();
			if ( cooldown < 0 )
			{
				throw new Exception("Forbidden value on cooldown = " + cooldown + ", it doesn't respect the following condition : cooldown < 0");
			}
		}
	}
}
