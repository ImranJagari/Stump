// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ShortcutObject.xml' the '05/11/2011 17:26:03'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class ShortcutObject : Shortcut
	{
		public const uint Id = 367;
		public override short TypeId
		{
			get
			{
				return 367;
			}
		}
		
		
		public ShortcutObject()
		{
		}
		
		public ShortcutObject(int slot)
			 : base(slot)
		{
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
		}
	}
}
