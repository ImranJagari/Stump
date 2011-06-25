// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'NotificationByServerMessage.xml' the '24/06/2011 12:04:49'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class NotificationByServerMessage : Message
	{
		public const uint Id = 6103;
		public override uint MessageId
		{
			get
			{
				return 6103;
			}
		}
		
		public ushort id;
		public string[] parameters;
		public bool forceOpen;
		
		public NotificationByServerMessage()
		{
		}
		
		public NotificationByServerMessage(ushort id, string[] parameters, bool forceOpen)
		{
			this.id = id;
			this.parameters = parameters;
			this.forceOpen = forceOpen;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort(id);
			writer.WriteUShort((ushort)parameters.Length);
			for (int i = 0; i < parameters.Length; i++)
			{
				writer.WriteUTF(parameters[i]);
			}
			writer.WriteBoolean(forceOpen);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			id = reader.ReadUShort();
			if ( id < 0 || id > 65535 )
			{
				throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 0 || id > 65535");
			}
			int limit = reader.ReadUShort();
			parameters = new string[limit];
			for (int i = 0; i < limit; i++)
			{
				parameters[i] = reader.ReadUTF();
			}
			forceOpen = reader.ReadBoolean();
		}
	}
}
