// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'TaxCollectorDialogQuestionExtendedMessage.xml' the '24/06/2011 12:04:50'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class TaxCollectorDialogQuestionExtendedMessage : TaxCollectorDialogQuestionBasicMessage
	{
		public const uint Id = 5615;
		public override uint MessageId
		{
			get
			{
				return 5615;
			}
		}
		
		public short maxPods;
		public short prospecting;
		public short wisdom;
		public byte taxCollectorsCount;
		public int taxCollectorAttack;
		public int kamas;
		public double experience;
		public int pods;
		public int itemsValue;
		
		public TaxCollectorDialogQuestionExtendedMessage()
		{
		}
		
		public TaxCollectorDialogQuestionExtendedMessage(Types.BasicGuildInformations guildInfo, short maxPods, short prospecting, short wisdom, byte taxCollectorsCount, int taxCollectorAttack, int kamas, double experience, int pods, int itemsValue)
			 : base(guildInfo)
		{
			this.maxPods = maxPods;
			this.prospecting = prospecting;
			this.wisdom = wisdom;
			this.taxCollectorsCount = taxCollectorsCount;
			this.taxCollectorAttack = taxCollectorAttack;
			this.kamas = kamas;
			this.experience = experience;
			this.pods = pods;
			this.itemsValue = itemsValue;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteShort(maxPods);
			writer.WriteShort(prospecting);
			writer.WriteShort(wisdom);
			writer.WriteByte(taxCollectorsCount);
			writer.WriteInt(taxCollectorAttack);
			writer.WriteInt(kamas);
			writer.WriteDouble(experience);
			writer.WriteInt(pods);
			writer.WriteInt(itemsValue);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			maxPods = reader.ReadShort();
			if ( maxPods < 0 )
			{
				throw new Exception("Forbidden value on maxPods = " + maxPods + ", it doesn't respect the following condition : maxPods < 0");
			}
			prospecting = reader.ReadShort();
			if ( prospecting < 0 )
			{
				throw new Exception("Forbidden value on prospecting = " + prospecting + ", it doesn't respect the following condition : prospecting < 0");
			}
			wisdom = reader.ReadShort();
			if ( wisdom < 0 )
			{
				throw new Exception("Forbidden value on wisdom = " + wisdom + ", it doesn't respect the following condition : wisdom < 0");
			}
			taxCollectorsCount = reader.ReadByte();
			if ( taxCollectorsCount < 0 )
			{
				throw new Exception("Forbidden value on taxCollectorsCount = " + taxCollectorsCount + ", it doesn't respect the following condition : taxCollectorsCount < 0");
			}
			taxCollectorAttack = reader.ReadInt();
			kamas = reader.ReadInt();
			if ( kamas < 0 )
			{
				throw new Exception("Forbidden value on kamas = " + kamas + ", it doesn't respect the following condition : kamas < 0");
			}
			experience = reader.ReadDouble();
			if ( experience < 0 )
			{
				throw new Exception("Forbidden value on experience = " + experience + ", it doesn't respect the following condition : experience < 0");
			}
			pods = reader.ReadInt();
			if ( pods < 0 )
			{
				throw new Exception("Forbidden value on pods = " + pods + ", it doesn't respect the following condition : pods < 0");
			}
			itemsValue = reader.ReadInt();
			if ( itemsValue < 0 )
			{
				throw new Exception("Forbidden value on itemsValue = " + itemsValue + ", it doesn't respect the following condition : itemsValue < 0");
			}
		}
	}
}
