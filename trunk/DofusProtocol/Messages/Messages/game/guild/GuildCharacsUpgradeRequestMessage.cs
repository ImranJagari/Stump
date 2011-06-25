// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GuildCharacsUpgradeRequestMessage.xml' the '24/06/2011 12:04:52'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GuildCharacsUpgradeRequestMessage : Message
	{
		public const uint Id = 5706;
		public override uint MessageId
		{
			get
			{
				return 5706;
			}
		}
		
		public byte charaTypeTarget;
		
		public GuildCharacsUpgradeRequestMessage()
		{
		}
		
		public GuildCharacsUpgradeRequestMessage(byte charaTypeTarget)
		{
			this.charaTypeTarget = charaTypeTarget;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteByte(charaTypeTarget);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			charaTypeTarget = reader.ReadByte();
			if ( charaTypeTarget < 0 )
			{
				throw new Exception("Forbidden value on charaTypeTarget = " + charaTypeTarget + ", it doesn't respect the following condition : charaTypeTarget < 0");
			}
		}
	}
}
