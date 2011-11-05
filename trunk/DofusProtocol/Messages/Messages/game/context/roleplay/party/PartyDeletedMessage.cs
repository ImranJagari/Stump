// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PartyDeletedMessage.xml' the '05/11/2011 17:25:52'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PartyDeletedMessage : AbstractPartyMessage
	{
		public const uint Id = 6261;
		public override uint MessageId
		{
			get
			{
				return 6261;
			}
		}
		
		
		public PartyDeletedMessage()
		{
		}
		
		public PartyDeletedMessage(int partyId)
			 : base(partyId)
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
