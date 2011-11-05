// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'EmoteRemoveMessage.xml' the '05/11/2011 17:25:50'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class EmoteRemoveMessage : Message
	{
		public const uint Id = 5687;
		public override uint MessageId
		{
			get
			{
				return 5687;
			}
		}
		
		public sbyte emoteId;
		
		public EmoteRemoveMessage()
		{
		}
		
		public EmoteRemoveMessage(sbyte emoteId)
		{
			this.emoteId = emoteId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(emoteId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			emoteId = reader.ReadSByte();
			if ( emoteId < 0 )
			{
				throw new Exception("Forbidden value on emoteId = " + emoteId + ", it doesn't respect the following condition : emoteId < 0");
			}
		}
	}
}
