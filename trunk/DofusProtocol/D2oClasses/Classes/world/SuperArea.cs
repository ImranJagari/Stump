
// Generated on 03/02/2013 21:17:47
using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("SuperAreas")]
    [Serializable]
    public class SuperArea : IDataObject, IIndexedData
    {
        private const String MODULE = "SuperAreas";
        public int id;
        public uint nameId;
        public uint worldmapId;

        int IIndexedData.Id
        {
            get { return (int)id; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public uint NameId
        {
            get { return nameId; }
            set { nameId = value; }
        }

        public uint WorldmapId
        {
            get { return worldmapId; }
            set { worldmapId = value; }
        }

    }
}