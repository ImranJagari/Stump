// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PrismFightDefendersStateMessage.xml' the '05/11/2011 17:25:59'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

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
		public IEnumerable<Types.CharacterMinimalPlusLookAndGradeInformations> mainFighters;
		public IEnumerable<Types.CharacterMinimalPlusLookAndGradeInformations> reserveFighters;
		
		public PrismFightDefendersStateMessage()
		{
		}
		
		public PrismFightDefendersStateMessage(double fightId, IEnumerable<Types.CharacterMinimalPlusLookAndGradeInformations> mainFighters, IEnumerable<Types.CharacterMinimalPlusLookAndGradeInformations> reserveFighters)
		{
			this.fightId = fightId;
			this.mainFighters = mainFighters;
			this.reserveFighters = reserveFighters;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteDouble(fightId);
			writer.WriteUShort((ushort)mainFighters.Count());
			foreach (var entry in mainFighters)
			{
				entry.Serialize(writer);
			}
			writer.WriteUShort((ushort)reserveFighters.Count());
			foreach (var entry in reserveFighters)
			{
				entry.Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			fightId = reader.ReadDouble();
			int limit = reader.ReadUShort();
			mainFighters = new Types.CharacterMinimalPlusLookAndGradeInformations[limit];
			for (int i = 0; i < limit; i++)
			{
				(mainFighters as Types.CharacterMinimalPlusLookAndGradeInformations[])[i] = new Types.CharacterMinimalPlusLookAndGradeInformations();
				(mainFighters as Types.CharacterMinimalPlusLookAndGradeInformations[])[i].Deserialize(reader);
			}
			limit = reader.ReadUShort();
			reserveFighters = new Types.CharacterMinimalPlusLookAndGradeInformations[limit];
			for (int i = 0; i < limit; i++)
			{
				(reserveFighters as Types.CharacterMinimalPlusLookAndGradeInformations[])[i] = new Types.CharacterMinimalPlusLookAndGradeInformations();
				(reserveFighters as Types.CharacterMinimalPlusLookAndGradeInformations[])[i].Deserialize(reader);
			}
		}
	}
}
