// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionFightSpellImmunityMessage.xml' the '05/11/2011 17:25:45'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameActionFightSpellImmunityMessage : AbstractGameActionMessage
	{
		public const uint Id = 6221;
		public override uint MessageId
		{
			get
			{
				return 6221;
			}
		}
		
		public int targetId;
		public int spellId;
		
		public GameActionFightSpellImmunityMessage()
		{
		}
		
		public GameActionFightSpellImmunityMessage(short actionId, int sourceId, int targetId, int spellId)
			 : base(actionId, sourceId)
		{
			this.targetId = targetId;
			this.spellId = spellId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(targetId);
			writer.WriteInt(spellId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			targetId = reader.ReadInt();
			spellId = reader.ReadInt();
			if ( spellId < 0 )
			{
				throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
			}
		}
	}
}
