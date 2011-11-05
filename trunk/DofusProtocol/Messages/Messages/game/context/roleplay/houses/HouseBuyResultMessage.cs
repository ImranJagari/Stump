// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'HouseBuyResultMessage.xml' the '05/11/2011 17:25:50'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class HouseBuyResultMessage : Message
	{
		public const uint Id = 5735;
		public override uint MessageId
		{
			get
			{
				return 5735;
			}
		}
		
		public int houseId;
		public bool bought;
		public int realPrice;
		
		public HouseBuyResultMessage()
		{
		}
		
		public HouseBuyResultMessage(int houseId, bool bought, int realPrice)
		{
			this.houseId = houseId;
			this.bought = bought;
			this.realPrice = realPrice;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(houseId);
			writer.WriteBoolean(bought);
			writer.WriteInt(realPrice);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			houseId = reader.ReadInt();
			if ( houseId < 0 )
			{
				throw new Exception("Forbidden value on houseId = " + houseId + ", it doesn't respect the following condition : houseId < 0");
			}
			bought = reader.ReadBoolean();
			realPrice = reader.ReadInt();
			if ( realPrice < 0 )
			{
				throw new Exception("Forbidden value on realPrice = " + realPrice + ", it doesn't respect the following condition : realPrice < 0");
			}
		}
	}
}
