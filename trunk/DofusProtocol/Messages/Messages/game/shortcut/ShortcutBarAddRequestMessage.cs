// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ShortcutBarAddRequestMessage.xml' the '05/11/2011 17:25:59'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ShortcutBarAddRequestMessage : Message
	{
		public const uint Id = 6225;
		public override uint MessageId
		{
			get
			{
				return 6225;
			}
		}
		
		public sbyte barType;
		public Types.Shortcut shortcut;
		
		public ShortcutBarAddRequestMessage()
		{
		}
		
		public ShortcutBarAddRequestMessage(sbyte barType, Types.Shortcut shortcut)
		{
			this.barType = barType;
			this.shortcut = shortcut;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(barType);
			writer.WriteShort(shortcut.TypeId);
			shortcut.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			barType = reader.ReadSByte();
			if ( barType < 0 )
			{
				throw new Exception("Forbidden value on barType = " + barType + ", it doesn't respect the following condition : barType < 0");
			}
			shortcut = Types.ProtocolTypeManager.GetInstance<Types.Shortcut>(reader.ReadShort());
			shortcut.Deserialize(reader);
		}
	}
}
