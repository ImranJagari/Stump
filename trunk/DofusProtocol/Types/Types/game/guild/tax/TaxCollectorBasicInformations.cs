// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'TaxCollectorBasicInformations.xml' the '14/06/2011 11:32:49'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class TaxCollectorBasicInformations
	{
		public const uint Id = 96;
		public short TypeId
		{
			get
			{
				return 96;
			}
		}
		
		public short firstNameId;
		public short lastNameId;
		public int mapId;
		
		public TaxCollectorBasicInformations()
		{
		}
		
		public TaxCollectorBasicInformations(short firstNameId, short lastNameId, int mapId)
		{
			this.firstNameId = firstNameId;
			this.lastNameId = lastNameId;
			this.mapId = mapId;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteShort(firstNameId);
			writer.WriteShort(lastNameId);
			writer.WriteInt(mapId);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			firstNameId = reader.ReadShort();
			if ( firstNameId < 0 )
			{
				throw new Exception("Forbidden value on firstNameId = " + firstNameId + ", it doesn't respect the following condition : firstNameId < 0");
			}
			lastNameId = reader.ReadShort();
			if ( lastNameId < 0 )
			{
				throw new Exception("Forbidden value on lastNameId = " + lastNameId + ", it doesn't respect the following condition : lastNameId < 0");
			}
			mapId = reader.ReadInt();
		}
	}
}