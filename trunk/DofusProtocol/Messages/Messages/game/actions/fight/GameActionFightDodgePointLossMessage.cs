// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionFightDodgePointLossMessage.xml' the '24/06/2011 12:04:45'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameActionFightDodgePointLossMessage : AbstractGameActionMessage
	{
		public const uint Id = 5828;
		public override uint MessageId
		{
			get
			{
				return 5828;
			}
		}
		
		public int targetId;
		public short amount;
		
		public GameActionFightDodgePointLossMessage()
		{
		}
		
		public GameActionFightDodgePointLossMessage(short actionId, int sourceId, int targetId, short amount)
			 : base(actionId, sourceId)
		{
			this.targetId = targetId;
			this.amount = amount;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(targetId);
			writer.WriteShort(amount);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			targetId = reader.ReadInt();
			amount = reader.ReadShort();
			if ( amount < 0 )
			{
				throw new Exception("Forbidden value on amount = " + amount + ", it doesn't respect the following condition : amount < 0");
			}
		}
	}
}
