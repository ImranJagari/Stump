﻿using System;
using Stump.DofusProtocol.D2oClasses;
using Stump.ORM.SubSonic.SQLGeneration.Schema;

namespace Stump.Server.WorldServer.Database.World
{
    public class AreaRecordRelator
    {
        public static string FetchQuery = "SELECT * FROM world_areas";
    }

    [TableName("world_areas")]
    [D2OClass("Area", "com.ankamagames.dofus.datacenter.world")]
    public sealed class AreaRecord : IAssignedByD2O
    {
        private string m_boundsCSV;
        private string m_name;

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

        public int SuperAreaId
        {
            get;
            set;
        }

        public Boolean ContainHouses
        {
            get;
            set;
        }

        public Boolean ContainPaddocks
        {
            get;
            set;
        }

        #region IAssignedByD2O Members

        public void AssignFields(object d2oObject)
        {
            var area = (Area) d2oObject;
            Id = area.id;
            NameId = area.nameId;
            SuperAreaId = area.superAreaId;
            ContainHouses = area.containHouses;
            ContainPaddocks = area.containPaddocks;
        }

        #endregion
    }
}