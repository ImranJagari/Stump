// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'MapComplementaryInformationsWithCoordsMessage.xml' the '24/06/2011 12:04:49'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class MapComplementaryInformationsWithCoordsMessage : MapComplementaryInformationsDataMessage
	{
		public const uint Id = 6268;
		public override uint MessageId
		{
			get
			{
				return 6268;
			}
		}
		
		public short worldX;
		public short worldY;
		
		public MapComplementaryInformationsWithCoordsMessage()
		{
		}
		
		public MapComplementaryInformationsWithCoordsMessage(short subareaId, int mapId, byte subareaAlignmentSide, Types.HouseInformations[] houses, Types.GameRolePlayActorInformations[] actors, Types.InteractiveElement[] interactiveElements, Types.StatedElement[] statedElements, Types.MapObstacle[] obstacles, Types.FightCommonInformations[] fights, short worldX, short worldY)
			 : base(subareaId, mapId, subareaAlignmentSide, houses, actors, interactiveElements, statedElements, obstacles, fights)
		{
			this.worldX = worldX;
			this.worldY = worldY;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteShort(worldX);
			writer.WriteShort(worldY);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			worldX = reader.ReadShort();
			if ( worldX < -255 || worldX > 255 )
			{
				throw new Exception("Forbidden value on worldX = " + worldX + ", it doesn't respect the following condition : worldX < -255 || worldX > 255");
			}
			worldY = reader.ReadShort();
			if ( worldY < -255 || worldY > 255 )
			{
				throw new Exception("Forbidden value on worldY = " + worldY + ", it doesn't respect the following condition : worldY < -255 || worldY > 255");
			}
		}
	}
}
