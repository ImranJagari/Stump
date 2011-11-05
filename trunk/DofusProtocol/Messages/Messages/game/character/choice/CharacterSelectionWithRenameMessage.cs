// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharacterSelectionWithRenameMessage.xml' the '05/11/2011 17:25:46'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class CharacterSelectionWithRenameMessage : CharacterSelectionMessage
	{
		public const uint Id = 6121;
		public override uint MessageId
		{
			get
			{
				return 6121;
			}
		}
		
		public string name;
		
		public CharacterSelectionWithRenameMessage()
		{
		}
		
		public CharacterSelectionWithRenameMessage(int id, string name)
			 : base(id)
		{
			this.name = name;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteUTF(name);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			name = reader.ReadUTF();
		}
	}
}
