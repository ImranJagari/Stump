// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ObjectEffect.xml' the '14/06/2011 11:32:48'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class ObjectEffect
	{
		public const uint Id = 76;
		public short TypeId
		{
			get
			{
				return 76;
			}
		}
		
		public short actionId;
		
		public ObjectEffect()
		{
		}
		
		public ObjectEffect(short actionId)
		{
			this.actionId = actionId;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteShort(actionId);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			actionId = reader.ReadShort();
			if ( actionId < 0 )
			{
				throw new Exception("Forbidden value on actionId = " + actionId + ", it doesn't respect the following condition : actionId < 0");
			}
		}
	}
}