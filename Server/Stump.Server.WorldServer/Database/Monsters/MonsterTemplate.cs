using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer.Database.I18n;
using Stump.Server.WorldServer.Game.Actors.Look;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Monster = Stump.DofusProtocol.D2oClasses.Monster;

namespace Stump.Server.WorldServer.Database.Monsters
{
    public class MonsterTemplateRelator
    {
        public static string FetchQuery = "SELECT * FROM monsters_templates";
    }

    [TableName("monsters_templates")]
    [D2OClass("Monster", "com.ankamagames.dofus.datacenter.monsters")]
    public sealed class MonsterTemplate : IAssignedByD2O, IAutoGeneratedRecord
    {
        private List<DroppableItem> m_droppableItems;
        private ActorLook m_entityLook;
        private List<MonsterGrade> m_grades;
        private string m_lookAsString;
        private string m_name;

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

        public string Name
        {
            get { return m_name ?? (m_name = TextManager.Instance.GetText(NameId)); }
        }

        public uint GfxId
        {
            get;
            set;
        }

        public int Race
        {
            get;
            set;
        }

        public int MinDroppedKamas
        {
            get;
            set;
        }

        public int MaxDroppedKamas
        {
            get;
            set;
        }

        [Ignore]
        public List<DroppableItem> DroppableItems
        {
            get { return m_droppableItems ?? (m_droppableItems = MonsterManager.Instance.GetMonsterDroppableItems(Id)); }
        }

        [Ignore]
        public List<MonsterGrade> Grades
        {
            get { return m_grades ?? (m_grades = MonsterManager.Instance.GetMonsterGrades(Id)); }
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

        public Boolean UseSummonSlot
        {
            get;
            set;
        }

        public Boolean UseBombSlot
        {
            get;
            set;
        }

        public Boolean CanPlay
        {
            get;
            set;
        }

        public Boolean CanTackle
        {
            get;
            set;
        }

        public Boolean IsBoss
        {
            get;
            set;
        }

        public Boolean IsActive
        {
            get;
            set;
        }

        #region IAssignedByD2O Members

        public void AssignFields(object d2oObject)
        {
            var monster = (Monster) d2oObject;
            Id = monster.id;
            NameId = monster.nameId;
            GfxId = monster.gfxId;
            Race = monster.race;
            LookAsString = monster.look;
            UseSummonSlot = monster.useSummonSlot;
            UseBombSlot = monster.useBombSlot;
            CanPlay = monster.canPlay;
            CanTackle = monster.canTackle;
            IsBoss = monster.isBoss;
            IsActive = true;
        }

        #endregion
    }
}