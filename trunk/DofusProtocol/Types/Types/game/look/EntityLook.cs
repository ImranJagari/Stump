// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'EntityLook.xml' the '14/06/2011 11:32:50'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class EntityLook
	{
		public const uint Id = 55;
		public short TypeId
		{
			get
			{
				return 55;
			}
		}
		
		public short bonesId;
		public short[] skins;
		public int[] indexedColors;
		public short[] scales;
		public Types.SubEntity[] subentities;
		
		public EntityLook()
		{
		}
		
		public EntityLook(short bonesId, short[] skins, int[] indexedColors, short[] scales, Types.SubEntity[] subentities)
		{
			this.bonesId = bonesId;
			this.skins = skins;
			this.indexedColors = indexedColors;
			this.scales = scales;
			this.subentities = subentities;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteShort(bonesId);
			writer.WriteUShort((ushort)skins.Length);
			for (int i = 0; i < skins.Length; i++)
			{
				writer.WriteShort(skins[i]);
			}
			writer.WriteUShort((ushort)indexedColors.Length);
			for (int i = 0; i < indexedColors.Length; i++)
			{
				writer.WriteInt(indexedColors[i]);
			}
			writer.WriteUShort((ushort)scales.Length);
			for (int i = 0; i < scales.Length; i++)
			{
				writer.WriteShort(scales[i]);
			}
			writer.WriteUShort((ushort)subentities.Length);
			for (int i = 0; i < subentities.Length; i++)
			{
				subentities[i].Serialize(writer);
			}
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			bonesId = reader.ReadShort();
			if ( bonesId < 0 )
			{
				throw new Exception("Forbidden value on bonesId = " + bonesId + ", it doesn't respect the following condition : bonesId < 0");
			}
			int limit = reader.ReadUShort();
			skins = new short[limit];
			for (int i = 0; i < limit; i++)
			{
				skins[i] = reader.ReadShort();
			}
			limit = reader.ReadUShort();
			indexedColors = new int[limit];
			for (int i = 0; i < limit; i++)
			{
				indexedColors[i] = reader.ReadInt();
			}
			limit = reader.ReadUShort();
			scales = new short[limit];
			for (int i = 0; i < limit; i++)
			{
				scales[i] = reader.ReadShort();
			}
			limit = reader.ReadUShort();
			subentities = new Types.SubEntity[limit];
			for (int i = 0; i < limit; i++)
			{
				subentities[i] = new Types.SubEntity();
				subentities[i].Deserialize(reader);
			}
		}
	}
}