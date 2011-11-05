// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeStartOkTaxCollectorMessage.xml' the '05/11/2011 17:25:57'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeStartOkTaxCollectorMessage : Message
	{
		public const uint Id = 5780;
		public override uint MessageId
		{
			get
			{
				return 5780;
			}
		}
		
		public int collectorId;
		public IEnumerable<Types.ObjectItem> objectsInfos;
		public int goldInfo;
		
		public ExchangeStartOkTaxCollectorMessage()
		{
		}
		
		public ExchangeStartOkTaxCollectorMessage(int collectorId, IEnumerable<Types.ObjectItem> objectsInfos, int goldInfo)
		{
			this.collectorId = collectorId;
			this.objectsInfos = objectsInfos;
			this.goldInfo = goldInfo;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(collectorId);
			writer.WriteUShort((ushort)objectsInfos.Count());
			foreach (var entry in objectsInfos)
			{
				entry.Serialize(writer);
			}
			writer.WriteInt(goldInfo);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			collectorId = reader.ReadInt();
			int limit = reader.ReadUShort();
			objectsInfos = new Types.ObjectItem[limit];
			for (int i = 0; i < limit; i++)
			{
				(objectsInfos as Types.ObjectItem[])[i] = new Types.ObjectItem();
				(objectsInfos as Types.ObjectItem[])[i].Deserialize(reader);
			}
			goldInfo = reader.ReadInt();
			if ( goldInfo < 0 )
			{
				throw new Exception("Forbidden value on goldInfo = " + goldInfo + ", it doesn't respect the following condition : goldInfo < 0");
			}
		}
	}
}
