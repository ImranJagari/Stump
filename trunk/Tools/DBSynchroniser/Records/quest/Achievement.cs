 


// Generated on 10/06/2013 18:02:18
using System;
using System.Collections.Generic;
using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;

namespace DBSynchroniser.Records
{
    [TableName("Achievements")]
    [D2OClass("Achievement", "com.ankamagames.dofus.datacenter.quest")]
    public class AchievementRecord : ID2ORecord
    {
        int ID2ORecord.Id
        {
            get { return (int)Id; }
        }
        private const String MODULE = "Achievements";
        public uint id;
        public uint nameId;
        public uint categoryId;
        public uint descriptionId;
        public int iconId;
        public uint points;
        public uint level;
        public uint order;
        public float kamasRatio;
        public float experienceRatio;
        public Boolean kamasScaleWithPlayerLevel;
        public List<int> objectiveIds;
        public List<int> rewardIds;

        [D2OIgnore]
        [PrimaryKey("Id", false)]
        public uint Id
        {
            get { return id; }
            set { id = value; }
        }

        [D2OIgnore]
        public uint NameId
        {
            get { return nameId; }
            set { nameId = value; }
        }

        [D2OIgnore]
        public uint CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }

        [D2OIgnore]
        public uint DescriptionId
        {
            get { return descriptionId; }
            set { descriptionId = value; }
        }

        [D2OIgnore]
        public int IconId
        {
            get { return iconId; }
            set { iconId = value; }
        }

        [D2OIgnore]
        public uint Points
        {
            get { return points; }
            set { points = value; }
        }

        [D2OIgnore]
        public uint Level
        {
            get { return level; }
            set { level = value; }
        }

        [D2OIgnore]
        public uint Order
        {
            get { return order; }
            set { order = value; }
        }

        [D2OIgnore]
        public float KamasRatio
        {
            get { return kamasRatio; }
            set { kamasRatio = value; }
        }

        [D2OIgnore]
        public float ExperienceRatio
        {
            get { return experienceRatio; }
            set { experienceRatio = value; }
        }

        [D2OIgnore]
        public Boolean KamasScaleWithPlayerLevel
        {
            get { return kamasScaleWithPlayerLevel; }
            set { kamasScaleWithPlayerLevel = value; }
        }

        [D2OIgnore]
        [Ignore]
        public List<int> ObjectiveIds
        {
            get { return objectiveIds; }
            set
            {
                objectiveIds = value;
                m_objectiveIdsBin = value == null ? null : value.ToBinary();
            }
        }

        private byte[] m_objectiveIdsBin;
        [D2OIgnore]
        public byte[] ObjectiveIdsBin
        {
            get { return m_objectiveIdsBin; }
            set
            {
                m_objectiveIdsBin = value;
                objectiveIds = value == null ? null : value.ToObject<List<int>>();
            }
        }

        [D2OIgnore]
        [Ignore]
        public List<int> RewardIds
        {
            get { return rewardIds; }
            set
            {
                rewardIds = value;
                m_rewardIdsBin = value == null ? null : value.ToBinary();
            }
        }

        private byte[] m_rewardIdsBin;
        [D2OIgnore]
        public byte[] RewardIdsBin
        {
            get { return m_rewardIdsBin; }
            set
            {
                m_rewardIdsBin = value;
                rewardIds = value == null ? null : value.ToObject<List<int>>();
            }
        }

        public virtual void AssignFields(object obj)
        {
            var castedObj = (Achievement)obj;
            
            Id = castedObj.id;
            NameId = castedObj.nameId;
            CategoryId = castedObj.categoryId;
            DescriptionId = castedObj.descriptionId;
            IconId = castedObj.iconId;
            Points = castedObj.points;
            Level = castedObj.level;
            Order = castedObj.order;
            KamasRatio = castedObj.kamasRatio;
            ExperienceRatio = castedObj.experienceRatio;
            KamasScaleWithPlayerLevel = castedObj.kamasScaleWithPlayerLevel;
            ObjectiveIds = castedObj.objectiveIds;
            RewardIds = castedObj.rewardIds;
        }
        
        public virtual object CreateObject(object parent = null)
        {
            
            var obj = parent != null ? (Achievement)parent : new Achievement();
            obj.id = Id;
            obj.nameId = NameId;
            obj.categoryId = CategoryId;
            obj.descriptionId = DescriptionId;
            obj.iconId = IconId;
            obj.points = Points;
            obj.level = Level;
            obj.order = Order;
            obj.kamasRatio = KamasRatio;
            obj.experienceRatio = ExperienceRatio;
            obj.kamasScaleWithPlayerLevel = KamasScaleWithPlayerLevel;
            obj.objectiveIds = ObjectiveIds;
            obj.rewardIds = RewardIds;
            return obj;
        
        }
    }
}