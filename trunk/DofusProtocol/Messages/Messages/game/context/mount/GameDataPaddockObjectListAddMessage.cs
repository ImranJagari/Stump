// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameDataPaddockObjectListAddMessage.xml' the '24/06/2011 12:04:49'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameDataPaddockObjectListAddMessage : Message
	{
		public const uint Id = 5992;
		public override uint MessageId
		{
			get
			{
				return 5992;
			}
		}
		
		public Types.PaddockItem[] paddockItemDescription;
		
		public GameDataPaddockObjectListAddMessage()
		{
		}
		
		public GameDataPaddockObjectListAddMessage(Types.PaddockItem[] paddockItemDescription)
		{
			this.paddockItemDescription = paddockItemDescription;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)paddockItemDescription.Length);
			for (int i = 0; i < paddockItemDescription.Length; i++)
			{
				paddockItemDescription[i].Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			paddockItemDescription = new Types.PaddockItem[limit];
			for (int i = 0; i < limit; i++)
			{
				paddockItemDescription[i] = new Types.PaddockItem();
				paddockItemDescription[i].Deserialize(reader);
			}
		}
	}
}
