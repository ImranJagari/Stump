// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'Item.xml' the '14/06/2011 11:32:48'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class Item
	{
		public const uint Id = 7;
		public short TypeId
		{
			get
			{
				return 7;
			}
		}
		
		
		public virtual void Serialize(IDataWriter writer)
		{
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
		}
	}
}