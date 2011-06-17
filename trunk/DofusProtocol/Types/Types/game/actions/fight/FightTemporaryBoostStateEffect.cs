// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'FightTemporaryBoostStateEffect.xml' the '14/06/2011 11:32:44'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class FightTemporaryBoostStateEffect : FightTemporaryBoostEffect
	{
		public const uint Id = 214;
		public short TypeId
		{
			get
			{
				return 214;
			}
		}
		
		public short stateId;
		
		public FightTemporaryBoostStateEffect()
		{
		}
		
		public FightTemporaryBoostStateEffect(int uid, int targetId, short turnDuration, byte dispelable, short spellId, short delta, short stateId)
			 : base(uid, targetId, turnDuration, dispelable, spellId, delta)
		{
			this.stateId = stateId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteShort(stateId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			stateId = reader.ReadShort();
		}
	}
}