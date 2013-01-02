using System;
using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;

namespace Stump.Server.WorldServer.Database
{
    public class SpellTemplateRelator
    {
        public static string FetchQuery = "SELECT * FROM spells_templates";
    }


    [TableName("spells_templates")]
    [D2OClass("Spell", "com.ankamagames.dofus.datacenter.spells")]
    public sealed class SpellTemplate : IAssignedByD2O, ISaveIntercepter, IAutoGeneratedRecord
    {
        private string m_description;
        private string m_name;
        private string m_spellLevelsIdsCSV;

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

        public uint DescriptionId
        {
            get;
            set;
        }


        public string Description
        {
            get { return m_description ?? (m_description = TextManager.Instance.GetText(NameId)); }
        }

        public uint TypeId
        {
            get;
            set;
        }

        public String ScriptParams
        {
            get;
            set;
        }

        public String ScriptParamsCritical
        {
            get;
            set;
        }

        public int ScriptId
        {
            get;
            set;
        }

        public int ScriptIdCritical
        {
            get;
            set;
        }

        public int IconId
        {
            get;
            set;
        }

        public string SpellLevelsIdsCSV
        {
            get { return m_spellLevelsIdsCSV; }
            set
            {
                m_spellLevelsIdsCSV = value;
                SpellLevelsIds = value.FromCSV<uint>(",");
            }
        }

        [Ignore]
        public uint[] SpellLevelsIds
        {
            get;
            set;
        }

        public Boolean UseParamCache
        {
            get;
            set;
        }

        #region IAssignedByD2O Members

        public void AssignFields(object d2oObject)
        {
            var spell = (Spell) d2oObject;
            Id = spell.id;
            NameId = spell.nameId;
            DescriptionId = spell.descriptionId;
            TypeId = spell.typeId;
            ScriptParams = spell.scriptParams;
            ScriptParamsCritical = spell.scriptParamsCritical;
            ScriptId = spell.scriptId;
            ScriptIdCritical = spell.scriptIdCritical;
            IconId = spell.iconId;
            SpellLevelsIds = spell.spellLevels.ToArray();
            UseParamCache = spell.useParamCache;
        }

        #endregion

        #region ISaveIntercepter Members

        public void BeforeSave(bool insert)
        {
            m_spellLevelsIdsCSV = SpellLevelsIds.ToCSV(",");
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0} - {1}", Id, Name);
        }
    }
}