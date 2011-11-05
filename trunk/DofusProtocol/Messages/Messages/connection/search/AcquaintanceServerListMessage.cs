// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'AcquaintanceServerListMessage.xml' the '05/11/2011 17:25:44'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class AcquaintanceServerListMessage : Message
	{
		public const uint Id = 6142;
		public override uint MessageId
		{
			get
			{
				return 6142;
			}
		}
		
		public IEnumerable<short> servers;
		
		public AcquaintanceServerListMessage()
		{
		}
		
		public AcquaintanceServerListMessage(IEnumerable<short> servers)
		{
			this.servers = servers;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)servers.Count());
			foreach (var entry in servers)
			{
				writer.WriteShort(entry);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			servers = new short[limit];
			for (int i = 0; i < limit; i++)
			{
				(servers as short[])[i] = reader.ReadShort();
			}
		}
	}
}
