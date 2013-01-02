using System;
using Stump.DofusProtocol.D2oClasses;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Point = System.Drawing.Point;

namespace Stump.Server.WorldServer.Database.World.Maps
{
    public class MapPositionRecordRelator
    {
        public static string FetchQuery = "SELECT * FROM world_maps_positions";
    }

    [TableName("world_maps_positions")]
    [D2OClass("MapPosition", "com.ankamagames.dofus.datacenter.world")]
    public sealed class MapPositionRecord : IAssignedByD2O, IAutoGeneratedRecord
    {
        private string m_name;
        private Point m_pos;

        public int Id
        {
            get;
            set;
        }

        public MapRecord Map
        {
            get;
            set;
        }

        public int PosX
        {
            get { return m_pos.X; }
            set { m_pos.X = value; }
        }

        public int PosY
        {
            get { return m_pos.Y; }
            set { m_pos.Y = value; }
        }

        public Point Pos
        {
            get { return m_pos; }
            set { m_pos = value; }
        }

        public Boolean Outdoor
        {
            get;
            set;
        }

        public int SubAreaId
        {
            get;
            set;
        }

        public int Capabilities
        {
            get;
            set;
        }

        public int WorldMap
        {
            get;
            set;
        }

        public int NameId
        {
            get;
            set;
        }

        public string Name
        {
            get { return m_name ?? (m_name = TextManager.Instance.GetText(NameId)); }
        }

        public bool HasPriorityOnWorldmap
        {
            get;
            set;
        }

        #region IAssignedByD2O Members

        public void AssignFields(object d2oObject)
        {
            var map = (MapPosition) d2oObject;
            Id = map.id;
            NameId = map.nameId;
            PosX = map.posX;
            PosY = map.posY;
            Outdoor = map.outdoor;
            SubAreaId = map.subAreaId;
            Capabilities = map.capabilities;
            WorldMap = map.worldMap;
            HasPriorityOnWorldmap = map.hasPriorityOnWorldmap;
        }

        #endregion
    }
}