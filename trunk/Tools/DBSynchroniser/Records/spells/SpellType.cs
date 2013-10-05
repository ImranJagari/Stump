 


// Generated on 10/06/2013 01:11:00
using System;
using System.Collections.Generic;
using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;

namespace DBSynchroniser.Records
{
    [D2OClass("SpellTypes")]
    public class SpellTypeRecord : ID2ORecord
    {
        private const String MODULE = "SpellTypes";
        public int id;
        public uint longNameId;
        public uint shortNameId;

        [PrimaryKey("Id", false)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public uint LongNameId
        {
            get { return longNameId; }
            set { longNameId = value; }
        }

        public uint ShortNameId
        {
            get { return shortNameId; }
            set { shortNameId = value; }
        }

        public void AssignFields(object obj)
        {
            var castedObj = (SpellType)obj;
            
            Id = castedObj.id;
            LongNameId = castedObj.longNameId;
            ShortNameId = castedObj.shortNameId;
        }
        
        public object CreateObject()
        {
            var obj = new SpellType();
            
            obj.id = Id;
            obj.longNameId = LongNameId;
            obj.shortNameId = ShortNameId;
            return obj;
        
        }
    }
}