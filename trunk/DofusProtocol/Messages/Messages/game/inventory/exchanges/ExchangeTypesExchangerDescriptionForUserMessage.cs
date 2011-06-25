// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeTypesExchangerDescriptionForUserMessage.xml' the '24/06/2011 12:04:55'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeTypesExchangerDescriptionForUserMessage : Message
	{
		public const uint Id = 5765;
		public override uint MessageId
		{
			get
			{
				return 5765;
			}
		}
		
		public int[] typeDescription;
		
		public ExchangeTypesExchangerDescriptionForUserMessage()
		{
		}
		
		public ExchangeTypesExchangerDescriptionForUserMessage(int[] typeDescription)
		{
			this.typeDescription = typeDescription;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)typeDescription.Length);
			for (int i = 0; i < typeDescription.Length; i++)
			{
				writer.WriteInt(typeDescription[i]);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			typeDescription = new int[limit];
			for (int i = 0; i < limit; i++)
			{
				typeDescription[i] = reader.ReadInt();
			}
		}
	}
}
