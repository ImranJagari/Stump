// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ObjectItemToSellInBid.xml' the '24/06/2011 12:04:58'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class ObjectItemToSellInBid : ObjectItemToSell
	{
		public const uint Id = 164;
		public override short TypeId
		{
			get
			{
				return 164;
			}
		}
		
		public short unsoldDelay;
		
		public ObjectItemToSellInBid()
		{
		}
		
		public ObjectItemToSellInBid(short objectGID, short powerRate, bool overMax, Types.ObjectEffect[] effects, int objectUID, int quantity, int objectPrice, short unsoldDelay)
			 : base(objectGID, powerRate, overMax, effects, objectUID, quantity, objectPrice)
		{
			this.unsoldDelay = unsoldDelay;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteShort(unsoldDelay);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			unsoldDelay = reader.ReadShort();
			if ( unsoldDelay < 0 )
			{
				throw new Exception("Forbidden value on unsoldDelay = " + unsoldDelay + ", it doesn't respect the following condition : unsoldDelay < 0");
			}
		}
	}
}
