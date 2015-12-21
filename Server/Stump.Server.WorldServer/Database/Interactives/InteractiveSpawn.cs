using System.Collections.Generic;
using NLog;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer.Game.Interactives;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;

namespace Stump.Server.WorldServer.Database.Interactives
{
    public class InteractiveSpawnRelator
    {
        public static string FetchQuery = "SELECT * FROM interactives_spawns " +
                                          "LEFT JOIN interactives_spawns_skills ON interactives_spawns_skills.InteractiveSpawnId=interactives_spawns.Id " +
                                          "LEFT JOIN interactives_skills ON interactives_skills.Id=interactives_spawns_skills.SkillId";

        private InteractiveSpawn m_current;

        public InteractiveSpawn Map(InteractiveSpawn spawn, InteractiveSpawnSkills binding, InteractiveCustomSkillRecord skill)
        {
            if (spawn == null)
                return m_current;

            if (m_current != null && m_current.Id == spawn.Id)
            {            
                if (binding.InteractiveSpawnId == m_current.Id && binding.SkillId == skill.Id)
                    m_current.CustomSkills.Add(skill);
                return null;
            }

            InteractiveSpawn previous = m_current;

            m_current = spawn;
            if (binding.InteractiveSpawnId == m_current.Id && binding.SkillId == skill.Id)
                m_current.CustomSkills.Add(skill);

            return previous;
        }
    }

    [TableName("interactives_spawns_skills")]
    public class InteractiveSpawnSkills : IAutoGeneratedRecord
    {
        [PrimaryKey("Id")]
        public int Id
        {
            get;
            set;
        }

        [Column("InteractiveSpawnId")]
        [Index]
        public int InteractiveSpawnId
        {
            get;
            set;
        }

        [Column("SkillId")]
        public int SkillId
        {
            get;
            set;
        }
    }

    [TableName("interactives_spawns")]
    public class InteractiveSpawn : IAutoGeneratedRecord
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private Map m_map;
        private InteractiveTemplate m_template;

        public InteractiveSpawn()
        {
            CustomSkills = new List<ISkillRecord>();
        }

        // Primitive properties
        [PrimaryKey("Id")]
        public int Id
        {
            get;
            set;
        }

        public int? TemplateId
        {
            get;
            set;
        }

        [Ignore]
        public InteractiveTemplate Template
        {
            get { return TemplateId.HasValue ? (m_template ?? (m_template = InteractiveManager.Instance.GetTemplate(TemplateId.Value))) : null; }
        }
        
        public int MapId
        {
            get;
            set;
        }

        public short CellId
        {
            get;
            set;
        }

        public int ElementId
        {
            get;
            set;
        }

        public bool Animated
        {
            get;
            set;
        }

        /// <summary>
        /// Custom skills if Template is null
        /// </summary>
        [Ignore]
        public List<ISkillRecord> CustomSkills
        {
            get;
            set;
        }

        public IEnumerable<ISkillRecord> GetSkills()
        {
            return CustomSkills.Count > 0 || Template == null ? CustomSkills : Template.GetSkills();
        }

        public Map GetMap()
        {
            return m_map ?? (m_map = Game.World.Instance.GetMap(MapId));
        }
    }
}