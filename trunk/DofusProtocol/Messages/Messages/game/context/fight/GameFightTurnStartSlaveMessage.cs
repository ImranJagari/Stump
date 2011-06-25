// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameFightTurnStartSlaveMessage.xml' the '24/06/2011 12:04:48'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameFightTurnStartSlaveMessage : GameFightTurnStartMessage
	{
		public const uint Id = 6213;
		public override uint MessageId
		{
			get
			{
				return 6213;
			}
		}
		
		public int idSummoner;
		
		public GameFightTurnStartSlaveMessage()
		{
		}
		
		public GameFightTurnStartSlaveMessage(int id, int waitTime, int idSummoner)
			 : base(id, waitTime)
		{
			this.idSummoner = idSummoner;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(idSummoner);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			idSummoner = reader.ReadInt();
		}
	}
}
