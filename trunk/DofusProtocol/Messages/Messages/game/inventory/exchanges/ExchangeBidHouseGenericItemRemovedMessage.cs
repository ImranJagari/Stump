// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeBidHouseGenericItemRemovedMessage.xml' the '05/11/2011 17:25:56'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeBidHouseGenericItemRemovedMessage : Message
	{
		public const uint Id = 5948;
		public override uint MessageId
		{
			get
			{
				return 5948;
			}
		}
		
		public int objGenericId;
		
		public ExchangeBidHouseGenericItemRemovedMessage()
		{
		}
		
		public ExchangeBidHouseGenericItemRemovedMessage(int objGenericId)
		{
			this.objGenericId = objGenericId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(objGenericId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			objGenericId = reader.ReadInt();
		}
	}
}
