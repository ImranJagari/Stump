// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'AbstractGameActionWithAckMessage.xml' the '05/11/2011 17:25:44'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class AbstractGameActionWithAckMessage : AbstractGameActionMessage
	{
		public const uint Id = 1001;
		public override uint MessageId
		{
			get
			{
				return 1001;
			}
		}
		
		public short waitAckId;
		
		public AbstractGameActionWithAckMessage()
		{
		}
		
		public AbstractGameActionWithAckMessage(short actionId, int sourceId, short waitAckId)
			 : base(actionId, sourceId)
		{
			this.waitAckId = waitAckId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteShort(waitAckId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			waitAckId = reader.ReadShort();
		}
	}
}
