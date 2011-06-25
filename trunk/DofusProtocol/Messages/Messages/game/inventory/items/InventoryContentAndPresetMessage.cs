// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'InventoryContentAndPresetMessage.xml' the '24/06/2011 12:04:55'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class InventoryContentAndPresetMessage : InventoryContentMessage
	{
		public const uint Id = 6162;
		public override uint MessageId
		{
			get
			{
				return 6162;
			}
		}
		
		public Types.Preset[] presets;
		
		public InventoryContentAndPresetMessage()
		{
		}
		
		public InventoryContentAndPresetMessage(Types.ObjectItem[] objects, int kamas, Types.Preset[] presets)
			 : base(objects, kamas)
		{
			this.presets = presets;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteUShort((ushort)presets.Length);
			for (int i = 0; i < presets.Length; i++)
			{
				presets[i].Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			int limit = reader.ReadUShort();
			presets = new Types.Preset[limit];
			for (int i = 0; i < limit; i++)
			{
				presets[i] = new Types.Preset();
				presets[i].Deserialize(reader);
			}
		}
	}
}
