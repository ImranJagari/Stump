// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'AlignmentBonusInformations.xml' the '24/06/2011 12:04:59'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class AlignmentBonusInformations
	{
		public const uint Id = 135;
		public virtual short TypeId
		{
			get
			{
				return 135;
			}
		}
		
		public int pctbonus;
		public double grademult;
		
		public AlignmentBonusInformations()
		{
		}
		
		public AlignmentBonusInformations(int pctbonus, double grademult)
		{
			this.pctbonus = pctbonus;
			this.grademult = grademult;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteInt(pctbonus);
			writer.WriteDouble(grademult);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			pctbonus = reader.ReadInt();
			if ( pctbonus < 0 )
			{
				throw new Exception("Forbidden value on pctbonus = " + pctbonus + ", it doesn't respect the following condition : pctbonus < 0");
			}
			grademult = reader.ReadDouble();
		}
	}
}
