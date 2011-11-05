// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameRolePlayActorInformations.xml' the '05/11/2011 17:26:01'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class GameRolePlayActorInformations : GameContextActorInformations
	{
		public const uint Id = 141;
		public override short TypeId
		{
			get
			{
				return 141;
			}
		}
		
		
		public GameRolePlayActorInformations()
		{
		}
		
		public GameRolePlayActorInformations(int contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition)
			 : base(contextualId, look, disposition)
		{
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
		}
	}
}
