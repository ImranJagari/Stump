// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharacterSelectionMessage.xml' the '05/11/2011 17:25:46'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class CharacterSelectionMessage : Message
	{
		public const uint Id = 152;
		public override uint MessageId
		{
			get
			{
				return 152;
			}
		}
		
		public int id;
		
		public CharacterSelectionMessage()
		{
		}
		
		public CharacterSelectionMessage(int id)
		{
			this.id = id;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(id);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			id = reader.ReadInt();
			if ( id < 1 || id > 2147483647 )
			{
				throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 1 || id > 2147483647");
			}
		}
	}
}
