// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'StartupActionsListMessage.xml' the '05/11/2011 17:26:00'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class StartupActionsListMessage : Message
	{
		public const uint Id = 1301;
		public override uint MessageId
		{
			get
			{
				return 1301;
			}
		}
		
		public IEnumerable<Types.StartupActionAddObject> actions;
		
		public StartupActionsListMessage()
		{
		}
		
		public StartupActionsListMessage(IEnumerable<Types.StartupActionAddObject> actions)
		{
			this.actions = actions;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)actions.Count());
			foreach (var entry in actions)
			{
				entry.Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			actions = new Types.StartupActionAddObject[limit];
			for (int i = 0; i < limit; i++)
			{
				(actions as Types.StartupActionAddObject[])[i] = new Types.StartupActionAddObject();
				(actions as Types.StartupActionAddObject[])[i].Deserialize(reader);
			}
		}
	}
}
