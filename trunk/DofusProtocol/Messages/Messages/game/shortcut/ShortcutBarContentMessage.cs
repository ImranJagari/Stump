// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ShortcutBarContentMessage.xml' the '24/06/2011 12:04:56'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ShortcutBarContentMessage : Message
	{
		public const uint Id = 6231;
		public override uint MessageId
		{
			get
			{
				return 6231;
			}
		}
		
		public byte barType;
		public Types.Shortcut[] shortcuts;
		
		public ShortcutBarContentMessage()
		{
		}
		
		public ShortcutBarContentMessage(byte barType, Types.Shortcut[] shortcuts)
		{
			this.barType = barType;
			this.shortcuts = shortcuts;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteByte(barType);
			writer.WriteUShort((ushort)shortcuts.Length);
			for (int i = 0; i < shortcuts.Length; i++)
			{
				writer.WriteShort(shortcuts[i].TypeId);
				shortcuts[i].Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			barType = reader.ReadByte();
			if ( barType < 0 )
			{
				throw new Exception("Forbidden value on barType = " + barType + ", it doesn't respect the following condition : barType < 0");
			}
			int limit = reader.ReadUShort();
			shortcuts = new Types.Shortcut[limit];
			for (int i = 0; i < limit; i++)
			{
				shortcuts[i] = Types.ProtocolTypeManager.GetInstance<Types.Shortcut>(reader.ReadShort());
				shortcuts[i].Deserialize(reader);
			}
		}
	}
}
