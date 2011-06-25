// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'BidExchangerObjectInfo.xml' the '24/06/2011 12:04:58'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class BidExchangerObjectInfo
	{
		public const uint Id = 122;
		public virtual short TypeId
		{
			get
			{
				return 122;
			}
		}
		
		public int objectUID;
		public short powerRate;
		public bool overMax;
		public Types.ObjectEffect[] effects;
		public int[] prices;
		
		public BidExchangerObjectInfo()
		{
		}
		
		public BidExchangerObjectInfo(int objectUID, short powerRate, bool overMax, Types.ObjectEffect[] effects, int[] prices)
		{
			this.objectUID = objectUID;
			this.powerRate = powerRate;
			this.overMax = overMax;
			this.effects = effects;
			this.prices = prices;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteInt(objectUID);
			writer.WriteShort(powerRate);
			writer.WriteBoolean(overMax);
			writer.WriteUShort((ushort)effects.Length);
			for (int i = 0; i < effects.Length; i++)
			{
				writer.WriteShort(effects[i].TypeId);
				effects[i].Serialize(writer);
			}
			writer.WriteUShort((ushort)prices.Length);
			for (int i = 0; i < prices.Length; i++)
			{
				writer.WriteInt(prices[i]);
			}
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			objectUID = reader.ReadInt();
			if ( objectUID < 0 )
			{
				throw new Exception("Forbidden value on objectUID = " + objectUID + ", it doesn't respect the following condition : objectUID < 0");
			}
			powerRate = reader.ReadShort();
			overMax = reader.ReadBoolean();
			int limit = reader.ReadUShort();
			effects = new Types.ObjectEffect[limit];
			for (int i = 0; i < limit; i++)
			{
				effects[i] = ProtocolTypeManager.GetInstance<Types.ObjectEffect>(reader.ReadShort());
				effects[i].Deserialize(reader);
			}
			limit = reader.ReadUShort();
			prices = new int[limit];
			for (int i = 0; i < limit; i++)
			{
				prices[i] = reader.ReadInt();
			}
		}
	}
}
