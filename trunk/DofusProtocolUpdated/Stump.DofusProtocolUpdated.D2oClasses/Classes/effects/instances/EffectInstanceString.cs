

// Generated on 12/12/2013 16:57:38
using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("EffectInstanceString", "com.ankamagames.dofus.datacenter.effects.instances")]
    [Serializable]
    public class EffectInstanceString : EffectInstance
    {
        public String text;
        [D2OIgnore]
        public String Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}