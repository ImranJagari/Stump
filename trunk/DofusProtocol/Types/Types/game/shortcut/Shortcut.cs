// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'Shortcut.xml' the '05/11/2011 17:26:03'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class Shortcut
	{
		public const uint Id = 369;
		public virtual short TypeId
		{
			get
			{
				return 369;
			}
		}
		
		public int slot;
		
		public Shortcut()
		{
		}
		
		public Shortcut(int slot)
		{
			this.slot = slot;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteInt(slot);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			slot = reader.ReadInt();
			if ( slot < 0 || slot > 99 )
			{
				throw new Exception("Forbidden value on slot = " + slot + ", it doesn't respect the following condition : slot < 0 || slot > 99");
			}
		}
	}
}
