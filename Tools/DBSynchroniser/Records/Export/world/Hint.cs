 


// Generated on 11/02/2013 14:55:51
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;

namespace DBSynchroniser.Records
{
    [TableName("Hints")]
    [D2OClass("Hint", "com.ankamagames.dofus.datacenter.world")]
    public class HintRecord : ID2ORecord, ISaveIntercepter
    {
        private const String MODULE = "Hints";
        public int id;
        public uint categoryId;
        public uint gfx;
        [I18NField]
        public uint nameId;
        public uint mapId;
        public uint realMapId;
        [I18NField]
        public int x;
        public int y;
        public Boolean outdoor;
        public int subareaId;

        int ID2ORecord.Id
        {
            get { return (int)id; }
        }


        [D2OIgnore]
        [PrimaryKey("Id", false)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [D2OIgnore]
        public uint CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }

        [D2OIgnore]
        public uint Gfx
        {
            get { return gfx; }
            set { gfx = value; }
        }

        [D2OIgnore]
        [I18NField]
        public uint NameId
        {
            get { return nameId; }
            set { nameId = value; }
        }

        [D2OIgnore]
        public uint MapId
        {
            get { return mapId; }
            set { mapId = value; }
        }

        [D2OIgnore]
        public uint RealMapId
        {
            get { return realMapId; }
            set { realMapId = value; }
        }

        [D2OIgnore]
        [I18NField]
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        [D2OIgnore]
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        [D2OIgnore]
        public Boolean Outdoor
        {
            get { return outdoor; }
            set { outdoor = value; }
        }

        [D2OIgnore]
        public int SubareaId
        {
            get { return subareaId; }
            set { subareaId = value; }
        }

        public virtual void AssignFields(object obj)
        {
            var castedObj = (Hint)obj;
            
            Id = castedObj.id;
            CategoryId = castedObj.categoryId;
            Gfx = castedObj.gfx;
            NameId = castedObj.nameId;
            MapId = castedObj.mapId;
            RealMapId = castedObj.realMapId;
            X = castedObj.x;
            Y = castedObj.y;
            Outdoor = castedObj.outdoor;
            SubareaId = castedObj.subareaId;
        }
        
        public virtual object CreateObject(object parent = null)
        {
            var obj = parent != null ? (Hint)parent : new Hint();
            obj.id = Id;
            obj.categoryId = CategoryId;
            obj.gfx = Gfx;
            obj.nameId = NameId;
            obj.mapId = MapId;
            obj.realMapId = RealMapId;
            obj.x = X;
            obj.y = Y;
            obj.outdoor = Outdoor;
            obj.subareaId = SubareaId;
            return obj;
        }
        
        public virtual void BeforeSave(bool insert)
        {
        
        }
    }
}