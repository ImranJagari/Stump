﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.Server.WorldServer.Game.Actors.Fight;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Targets
{
    public class UnknownCriterion : TargetCriterion
    {
        public UnknownCriterion(string criterion)
        {
            Criterion = criterion;
        }

        public string Criterion
        {
            get;
            set;
        }

        public override bool IsTargetValid(FightActor actor, SpellEffectHandler handler)
        {
            return true;
        }
    }
}
