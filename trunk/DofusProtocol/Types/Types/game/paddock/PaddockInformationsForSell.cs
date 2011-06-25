// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PaddockInformationsForSell.xml' the '24/06/2011 12:04:59'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class PaddockInformationsForSell
	{
		public const uint Id = 222;
		public virtual short TypeId
		{
			get
			{
				return 222;
			}
		}
		
		public string guildOwner;
		public short worldX;
		public short worldY;
		public short subAreaId;
		public byte nbMount;
		public byte nbObject;
		public int price;
		
		public PaddockInformationsForSell()
		{
		}
		
		public PaddockInformationsForSell(string guildOwner, short worldX, short worldY, short subAreaId, byte nbMount, byte nbObject, int price)
		{
			this.guildOwner = guildOwner;
			this.worldX = worldX;
			this.worldY = worldY;
			this.subAreaId = subAreaId;
			this.nbMount = nbMount;
			this.nbObject = nbObject;
			this.price = price;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteUTF(guildOwner);
			writer.WriteShort(worldX);
			writer.WriteShort(worldY);
			writer.WriteShort(subAreaId);
			writer.WriteByte(nbMount);
			writer.WriteByte(nbObject);
			writer.WriteInt(price);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			guildOwner = reader.ReadUTF();
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
			subAreaId = reader.ReadShort();
			if ( subAreaId < 0 )
			{
				throw new Exception("Forbidden value on subAreaId = " + subAreaId + ", it doesn't respect the following condition : subAreaId < 0");
			}
			nbMount = reader.ReadByte();
			nbObject = reader.ReadByte();
			price = reader.ReadInt();
			if ( price < 0 )
			{
				throw new Exception("Forbidden value on price = " + price + ", it doesn't respect the following condition : price < 0");
			}
		}
	}
}
