// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'FightResultAdditionalData.xml' the '14/06/2011 11:32:46'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class FightResultAdditionalData
	{
		public const uint Id = 191;
		public short TypeId
		{
			get
			{
				return 191;
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