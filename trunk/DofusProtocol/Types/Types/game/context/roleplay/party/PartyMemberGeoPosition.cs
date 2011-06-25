// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PartyMemberGeoPosition.xml' the '24/06/2011 12:04:58'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class PartyMemberGeoPosition
	{
		public const uint Id = 378;
		public virtual short TypeId
		{
			get
			{
				return 378;
			}
		}
		
		public int memberId;
		public short worldX;
		public short worldY;
		public int mapId;
		
		public PartyMemberGeoPosition()
		{
		}
		
		public PartyMemberGeoPosition(int memberId, short worldX, short worldY, int mapId)
		{
			this.memberId = memberId;
			this.worldX = worldX;
			this.worldY = worldY;
			this.mapId = mapId;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteInt(memberId);
			writer.WriteShort(worldX);
			writer.WriteShort(worldY);
			writer.WriteInt(mapId);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			memberId = reader.ReadInt();
			if ( memberId < 0 )
			{
				throw new Exception("Forbidden value on memberId = " + memberId + ", it doesn't respect the following condition : memberId < 0");
			}
			worldX = reader.ReadShort();
			if ( worldX < -255 || worldX > 255 )
			{
				throw new Exception("Forbidden value on worldX = " + worldX + ", it doesn't respect the following condition : worldX < -255 || worldX > 255");
			}
			worldY = reader.ReadShort();
			if ( worldY < -255 || worldY > 255 )
			{
				throw new Exception("Forbidden value on worldY = " + worldY + ", it doesn't respect the following condition : worldY < -255 || worldY > 255");
			}
			mapId = reader.ReadInt();
		}
	}
}
