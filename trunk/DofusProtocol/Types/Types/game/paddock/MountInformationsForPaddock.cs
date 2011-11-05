// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'MountInformationsForPaddock.xml' the '05/11/2011 17:26:03'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class MountInformationsForPaddock
	{
		public const uint Id = 184;
		public virtual short TypeId
		{
			get
			{
				return 184;
			}
		}
		
		public int modelId;
		public string name;
		public string ownerName;
		
		public MountInformationsForPaddock()
		{
		}
		
		public MountInformationsForPaddock(int modelId, string name, string ownerName)
		{
			this.modelId = modelId;
			this.name = name;
			this.ownerName = ownerName;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteInt(modelId);
			writer.WriteUTF(name);
			writer.WriteUTF(ownerName);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			modelId = reader.ReadInt();
			name = reader.ReadUTF();
			ownerName = reader.ReadUTF();
		}
	}
}
