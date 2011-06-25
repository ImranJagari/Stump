// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeMountStableBornAddMessage.xml' the '24/06/2011 12:04:54'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeMountStableBornAddMessage : ExchangeMountStableAddMessage
	{
		public const uint Id = 5966;
		public override uint MessageId
		{
			get
			{
				return 5966;
			}
		}
		
		
		public ExchangeMountStableBornAddMessage()
		{
		}
		
		public ExchangeMountStableBornAddMessage(Types.MountClientData mountDescription)
			 : base(mountDescription)
		{
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
		}
	}
}
