﻿using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer.Database.I18n;
using Stump.Server.WorldServer.Game.Actors.Look;

namespace Stump.Server.WorldServer.Database.Mounts
{
    public class MountTemplateRelator
    {
        public static string FetchQuery = "SELECT * FROM mounts_templates";
    }

    [TableName("mounts_templates")]
    [D2OClass("Mount", "com.ankamagames.dofus.datacenter.mounts")]
    public sealed class MountTemplate : IAssignedByD2O, IAutoGeneratedRecord
    {
        private ActorLook m_entityLook;
        private string m_name;
        private string m_lookAsString;

        [PrimaryKey("Id", false)]
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

        [Ignore]
        public string Name
        {
            get { return m_name ?? (m_name = TextManager.Instance.GetText(NameId)); }
        }

        public string LookAsString
        {
            get
            {
                if (EntityLook == null)
                    return string.Empty;

                if (string.IsNullOrEmpty(m_lookAsString))
                    m_lookAsString = EntityLook.ToString();

                return m_lookAsString;
            }
            set
            {
                m_lookAsString = value;

                if (!string.IsNullOrEmpty(value) && value != "null")
                    m_entityLook = ActorLook.Parse(m_lookAsString);
                else
                    m_entityLook = null;
            }
        }

        [Ignore]
        public ActorLook EntityLook
        {
            get { return m_entityLook; }
            set
            {
                m_entityLook = value;

                if (value != null)
                    m_lookAsString = value.ToString();
            }
        }

        public int PodsBase
        {
            get;
            set;
        }

        public int PodsPerLevel
        {
            get;
            set;
        }

        public int EnergyBase
        {
            get;
            set;
        }

        public int EnergyPerLevel
        {
            get;
            set;
        }

        public int MaturityBase
        {
            get;
            set;
        }

        public int FecondationTime
        {
            get;
            set;
        }

        public sbyte LearnCoefficient
        {
            get;
            set;
        }

        #region IAssignedByD2O Members

        public void AssignFields(object d2oObject)
        {
            var mount = (Mount)d2oObject;
            Id = (int)mount.id;
            NameId = mount.nameId;
            LookAsString = mount.look;
        }

        #endregion
    }
}
