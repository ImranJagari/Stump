// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionFightDispellSpellMessage.xml' the '24/06/2011 12:04:45'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameActionFightDispellSpellMessage : GameActionFightDispellMessage
	{
		public const uint Id = 6176;
		public override uint MessageId
		{
			get
			{
				return 6176;
			}
		}
		
		public int spellId;
		
		public GameActionFightDispellSpellMessage()
		{
		}
		
		public GameActionFightDispellSpellMessage(short actionId, int sourceId, int targetId, int spellId)
			 : base(actionId, sourceId, targetId)
		{
			this.spellId = spellId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(spellId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			spellId = reader.ReadInt();
			if ( spellId < 0 )
			{
				throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
			}
		}
	}
}
