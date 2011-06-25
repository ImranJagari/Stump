// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionFightChangeLookMessage.xml' the '24/06/2011 12:04:45'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameActionFightChangeLookMessage : AbstractGameActionMessage
	{
		public const uint Id = 5532;
		public override uint MessageId
		{
			get
			{
				return 5532;
			}
		}
		
		public int targetId;
		public Types.EntityLook entityLook;
		
		public GameActionFightChangeLookMessage()
		{
		}
		
		public GameActionFightChangeLookMessage(short actionId, int sourceId, int targetId, Types.EntityLook entityLook)
			 : base(actionId, sourceId)
		{
			this.targetId = targetId;
			this.entityLook = entityLook;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(targetId);
			entityLook.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			targetId = reader.ReadInt();
			entityLook = new Types.EntityLook();
			entityLook.Deserialize(reader);
		}
	}
}
