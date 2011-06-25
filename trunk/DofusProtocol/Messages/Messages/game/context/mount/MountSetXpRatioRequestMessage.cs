// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'MountSetXpRatioRequestMessage.xml' the '24/06/2011 12:04:49'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class MountSetXpRatioRequestMessage : Message
	{
		public const uint Id = 5989;
		public override uint MessageId
		{
			get
			{
				return 5989;
			}
		}
		
		public byte xpRatio;
		
		public MountSetXpRatioRequestMessage()
		{
		}
		
		public MountSetXpRatioRequestMessage(byte xpRatio)
		{
			this.xpRatio = xpRatio;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteByte(xpRatio);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			xpRatio = reader.ReadByte();
			if ( xpRatio < 0 )
			{
				throw new Exception("Forbidden value on xpRatio = " + xpRatio + ", it doesn't respect the following condition : xpRatio < 0");
			}
		}
	}
}
