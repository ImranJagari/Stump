
// Generated on 03/02/2013 21:17:46
using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("MonsterMiniBoss")]
    [Serializable]
    public class MonsterMiniBoss : IDataObject, IIndexedData
    {
        private const String MODULE = "MonsterMiniBoss";
        public int id;
        public int monsterReplacingId;

        int IIndexedData.Id
        {
            get { return (int)id; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int MonsterReplacingId
        {
            get { return monsterReplacingId; }
            set { monsterReplacingId = value; }
        }

    }
}