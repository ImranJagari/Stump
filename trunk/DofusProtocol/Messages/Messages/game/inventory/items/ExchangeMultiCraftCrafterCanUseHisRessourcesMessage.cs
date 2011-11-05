// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeMultiCraftCrafterCanUseHisRessourcesMessage.xml' the '05/11/2011 17:25:58'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeMultiCraftCrafterCanUseHisRessourcesMessage : Message
	{
		public const uint Id = 6020;
		public override uint MessageId
		{
			get
			{
				return 6020;
			}
		}
		
		public bool allowed;
		
		public ExchangeMultiCraftCrafterCanUseHisRessourcesMessage()
		{
		}
		
		public ExchangeMultiCraftCrafterCanUseHisRessourcesMessage(bool allowed)
		{
			this.allowed = allowed;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteBoolean(allowed);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			allowed = reader.ReadBoolean();
		}
	}
}
