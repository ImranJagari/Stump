// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'IgnoredOnlineInformations.xml' the '24/06/2011 12:04:59'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class IgnoredOnlineInformations : IgnoredInformations
	{
		public const uint Id = 105;
		public override short TypeId
		{
			get
			{
				return 105;
			}
		}
		
		public string playerName;
		public byte breed;
		public bool sex;
		
		public IgnoredOnlineInformations()
		{
		}
		
		public IgnoredOnlineInformations(string name, int id, string playerName, byte breed, bool sex)
			 : base(name, id)
		{
			this.playerName = playerName;
			this.breed = breed;
			this.sex = sex;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteUTF(playerName);
			writer.WriteByte(breed);
			writer.WriteBoolean(sex);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			playerName = reader.ReadUTF();
			breed = reader.ReadByte();
			if ( breed < (byte)Enums.PlayableBreedEnum.Feca || breed > (byte)Enums.PlayableBreedEnum.Zobal )
			{
				throw new Exception("Forbidden value on breed = " + breed + ", it doesn't respect the following condition : breed < (byte)Enums.PlayableBreedEnum.Feca || breed > (byte)Enums.PlayableBreedEnum.Zobal");
			}
			sex = reader.ReadBoolean();
		}
	}
}
