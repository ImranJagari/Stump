// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionFightThrowCharacterMessage.xml' the '24/06/2011 12:04:46'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameActionFightThrowCharacterMessage : AbstractGameActionMessage
	{
		public const uint Id = 5829;
		public override uint MessageId
		{
			get
			{
				return 5829;
			}
		}
		
		public int targetId;
		public short cellId;
		
		public GameActionFightThrowCharacterMessage()
		{
		}
		
		public GameActionFightThrowCharacterMessage(short actionId, int sourceId, int targetId, short cellId)
			 : base(actionId, sourceId)
		{
			this.targetId = targetId;
			this.cellId = cellId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(targetId);
			writer.WriteShort(cellId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			targetId = reader.ReadInt();
			cellId = reader.ReadShort();
			if ( cellId < -1 || cellId > 559 )
			{
				throw new Exception("Forbidden value on cellId = " + cellId + ", it doesn't respect the following condition : cellId < -1 || cellId > 559");
			}
		}
	}
}
