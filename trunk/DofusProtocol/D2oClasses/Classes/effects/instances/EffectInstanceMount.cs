
// Generated on 03/02/2013 21:17:44
using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("EffectInstanceMount")]
    [Serializable]
    public class EffectInstanceMount : EffectInstance, IIndexedData
    {
        public float date;
        public uint modelId;
        public uint mountId;

        public float Date
        {
            get { return date; }
            set { date = value; }
        }

        public uint ModelId
        {
            get { return modelId; }
            set { modelId = value; }
        }

        public uint MountId
        {
            get { return mountId; }
            set { mountId = value; }
        }

    }
}