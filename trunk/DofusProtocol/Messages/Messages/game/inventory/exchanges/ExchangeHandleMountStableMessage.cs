// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeHandleMountStableMessage.xml' the '24/06/2011 12:04:53'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeHandleMountStableMessage : Message
	{
		public const uint Id = 5965;
		public override uint MessageId
		{
			get
			{
				return 5965;
			}
		}
		
		public byte actionType;
		public int rideId;
		
		public ExchangeHandleMountStableMessage()
		{
		}
		
		public ExchangeHandleMountStableMessage(byte actionType, int rideId)
		{
			this.actionType = actionType;
			this.rideId = rideId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteByte(actionType);
			writer.WriteInt(rideId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			actionType = reader.ReadByte();
			rideId = reader.ReadInt();
			if ( rideId < 0 )
			{
				throw new Exception("Forbidden value on rideId = " + rideId + ", it doesn't respect the following condition : rideId < 0");
			}
		}
	}
}
