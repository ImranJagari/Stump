// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ObjectUseMultipleMessage.xml' the '05/11/2011 17:25:59'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ObjectUseMultipleMessage : ObjectUseMessage
	{
		public const uint Id = 6234;
		public override uint MessageId
		{
			get
			{
				return 6234;
			}
		}
		
		public int quantity;
		
		public ObjectUseMultipleMessage()
		{
		}
		
		public ObjectUseMultipleMessage(int objectUID, int quantity)
			 : base(objectUID)
		{
			this.quantity = quantity;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(quantity);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			quantity = reader.ReadInt();
			if ( quantity < 0 )
			{
				throw new Exception("Forbidden value on quantity = " + quantity + ", it doesn't respect the following condition : quantity < 0");
			}
		}
	}
}
