﻿using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer.Game.Maps;
using SubArea = Stump.DofusProtocol.D2oClasses.SubArea;

namespace Stump.Server.WorldServer.Database.World
{
    public class SubAreaRecordRelator
    {
        public static string FetchQuery = "SELECT * FROM world_subareas";
    }

    [TableName("world_subareas")]
    [D2OClass("SubArea", "com.ankamagames.dofus.datacenter.world")]
    public sealed class SubAreaRecord : IAssignedByD2O, ISaveIntercepter, IAutoGeneratedRecord
    {
        private uint[] m_customWorldMap;
        private string m_customWorldMapCSV;
        private uint[] m_mapsIds;
        private string m_mapsIdsCSV;
        private string m_name;
        private int[] m_shape;
        private string m_shapeCSV;

        public int Id
        {
            get;
            set;
        }

        public uint NameId
        {
            get;
            set;
        }


        public string Name
        {
            get { return m_name ?? (m_name = TextManager.Instance.GetText(NameId)); }
        }

        public int AreaId
        {
            get;
            set;
        }

        public string MapsIdsCSV
        {
            get { return m_mapsIdsCSV; }
            set
            {
                m_mapsIdsCSV = value;
                m_mapsIds = value.FromCSV<uint>(",");
            }
        }

        [Ignore]
        public uint[] MapsIds
        {
            get { return m_mapsIds; }
            set
            {
                m_mapsIds = value;
                m_mapsIdsCSV = value.ToCSV(",");
            }
        }

        public string ShapeCSV
        {
            get { return m_shapeCSV; }
            set
            {
                m_shapeCSV = value;
                m_shape = value.FromCSV<int>(",");
            }
        }

        [Ignore]
        public int[] Shape
        {
            get { return m_shape; }
            set
            {
                m_shape = value;
                m_shapeCSV = value.ToCSV(",");
            }
        }

        public string CustomWorldMapCSV
        {
            get { return m_customWorldMapCSV; }
            set
            {
                m_customWorldMapCSV = value;
                m_customWorldMap = value.FromCSV<uint>(",");
            }
        }

        [Ignore]
        public uint[] CustomWorldMap
        {
            get { return m_customWorldMap; }
            set
            {
                m_customWorldMap = value;
                m_customWorldMapCSV = value.ToCSV(",");
            }
        }

        public int PackId
        {
            get;
            set;
        }

        [SubSonicDefaultSetting(2)]
        public Difficulty Difficulty
        {
            get;
            set;
        }

        [SubSonicDefaultSetting(3)]
        public int SpawnsLimit
        {
            get;
            set;
        }

        public uint? CustomSpawnInterval
        {
            get;
            set;
        }

        #region IAssignedByD2O Members

        public void AssignFields(object d2oObject)
        {
            var subarea = (SubArea) d2oObject;
            Id = subarea.id;
            NameId = subarea.nameId;
            AreaId = subarea.areaId;
            MapsIds = subarea.mapIds.ToArray();
            Shape = subarea.shape.ToArray();
            CustomWorldMap = subarea.customWorldMap.ToArray();
            PackId = subarea.packId;
            Difficulty = Difficulty.Normal;
            SpawnsLimit = 3;
        }

        #endregion

        #region ISaveIntercepter Members

        public void BeforeSave(bool insert)
        {
            MapsIdsCSV = m_mapsIds.ToCSV(",");
            ShapeCSV = m_shape.ToCSV(",");
            CustomWorldMapCSV = m_customWorldMap.ToCSV(",");
        }

        #endregion
    }
}