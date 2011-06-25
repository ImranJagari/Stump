// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionFightPassNextTurnsMessage.xml' the '24/06/2011 12:04:46'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameActionFightPassNextTurnsMessage : AbstractGameActionMessage
	{
		public const uint Id = 5529;
		public override uint MessageId
		{
			get
			{
				return 5529;
			}
		}
		
		public int targetId;
		public byte turnCount;
		
		public GameActionFightPassNextTurnsMessage()
		{
		}
		
		public GameActionFightPassNextTurnsMessage(short actionId, int sourceId, int targetId, byte turnCount)
			 : base(actionId, sourceId)
		{
			this.targetId = targetId;
			this.turnCount = turnCount;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(targetId);
			writer.WriteByte(turnCount);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			targetId = reader.ReadInt();
			turnCount = reader.ReadByte();
			if ( turnCount < 0 )
			{
				throw new Exception("Forbidden value on turnCount = " + turnCount + ", it doesn't respect the following condition : turnCount < 0");
			}
		}
	}
}
