// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PrismFightDefendersStateMessage.xml' the '24/06/2011 12:04:56'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PrismFightDefendersStateMessage : Message
	{
		public const uint Id = 5899;
		public override uint MessageId
		{
			get
			{
				return 5899;
			}
		}
		
		public double fightId;
		public Types.CharacterMinimalPlusLookAndGradeInformations[] mainFighters;
		public Types.CharacterMinimalPlusLookAndGradeInformations[] reserveFighters;
		
		public PrismFightDefendersStateMessage()
		{
		}
		
		public PrismFightDefendersStateMessage(double fightId, Types.CharacterMinimalPlusLookAndGradeInformations[] mainFighters, Types.CharacterMinimalPlusLookAndGradeInformations[] reserveFighters)
		{
			this.fightId = fightId;
			this.mainFighters = mainFighters;
			this.reserveFighters = reserveFighters;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteDouble(fightId);
			writer.WriteUShort((ushort)mainFighters.Length);
			for (int i = 0; i < mainFighters.Length; i++)
			{
				mainFighters[i].Serialize(writer);
			}
			writer.WriteUShort((ushort)reserveFighters.Length);
			for (int i = 0; i < reserveFighters.Length; i++)
			{
				reserveFighters[i].Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			fightId = reader.ReadDouble();
			int limit = reader.ReadUShort();
			mainFighters = new Types.CharacterMinimalPlusLookAndGradeInformations[limit];
			for (int i = 0; i < limit; i++)
			{
				mainFighters[i] = new Types.CharacterMinimalPlusLookAndGradeInformations();
				mainFighters[i].Deserialize(reader);
			}
			limit = reader.ReadUShort();
			reserveFighters = new Types.CharacterMinimalPlusLookAndGradeInformations[limit];
			for (int i = 0; i < limit; i++)
			{
				reserveFighters[i] = new Types.CharacterMinimalPlusLookAndGradeInformations();
				reserveFighters[i].Deserialize(reader);
			}
		}
	}
}
