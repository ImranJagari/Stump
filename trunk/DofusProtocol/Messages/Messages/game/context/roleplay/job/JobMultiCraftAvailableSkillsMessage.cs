// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'JobMultiCraftAvailableSkillsMessage.xml' the '05/11/2011 17:25:51'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class JobMultiCraftAvailableSkillsMessage : JobAllowMultiCraftRequestMessage
	{
		public const uint Id = 5747;
		public override uint MessageId
		{
			get
			{
				return 5747;
			}
		}
		
		public int playerId;
		public IEnumerable<short> skills;
		
		public JobMultiCraftAvailableSkillsMessage()
		{
		}
		
		public JobMultiCraftAvailableSkillsMessage(bool enabled, int playerId, IEnumerable<short> skills)
			 : base(enabled)
		{
			this.playerId = playerId;
			this.skills = skills;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(playerId);
			writer.WriteUShort((ushort)skills.Count());
			foreach (var entry in skills)
			{
				writer.WriteShort(entry);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			playerId = reader.ReadInt();
			if ( playerId < 0 )
			{
				throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0");
			}
			int limit = reader.ReadUShort();
			skills = new short[limit];
			for (int i = 0; i < limit; i++)
			{
				(skills as short[])[i] = reader.ReadShort();
			}
		}
	}
}
