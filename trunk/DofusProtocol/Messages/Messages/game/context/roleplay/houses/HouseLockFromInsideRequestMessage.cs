// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'HouseLockFromInsideRequestMessage.xml' the '24/06/2011 12:04:50'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class HouseLockFromInsideRequestMessage : LockableChangeCodeMessage
	{
		public const uint Id = 5885;
		public override uint MessageId
		{
			get
			{
				return 5885;
			}
		}
		
		
		public HouseLockFromInsideRequestMessage()
		{
		}
		
		public HouseLockFromInsideRequestMessage(string code)
			 : base(code)
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
