// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ShortcutBarAddErrorMessage.xml' the '24/06/2011 12:04:56'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ShortcutBarAddErrorMessage : Message
	{
		public const uint Id = 6227;
		public override uint MessageId
		{
			get
			{
				return 6227;
			}
		}
		
		public byte error;
		
		public ShortcutBarAddErrorMessage()
		{
		}
		
		public ShortcutBarAddErrorMessage(byte error)
		{
			this.error = error;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteByte(error);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			error = reader.ReadByte();
			if ( error < 0 )
			{
				throw new Exception("Forbidden value on error = " + error + ", it doesn't respect the following condition : error < 0");
			}
		}
	}
}
