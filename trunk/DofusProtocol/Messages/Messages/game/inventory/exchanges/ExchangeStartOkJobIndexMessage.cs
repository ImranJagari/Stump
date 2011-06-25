// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeStartOkJobIndexMessage.xml' the '24/06/2011 12:04:55'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeStartOkJobIndexMessage : Message
	{
		public const uint Id = 5819;
		public override uint MessageId
		{
			get
			{
				return 5819;
			}
		}
		
		public int[] jobs;
		
		public ExchangeStartOkJobIndexMessage()
		{
		}
		
		public ExchangeStartOkJobIndexMessage(int[] jobs)
		{
			this.jobs = jobs;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)jobs.Length);
			for (int i = 0; i < jobs.Length; i++)
			{
				writer.WriteInt(jobs[i]);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			jobs = new int[limit];
			for (int i = 0; i < limit; i++)
			{
				jobs[i] = reader.ReadInt();
			}
		}
	}
}
