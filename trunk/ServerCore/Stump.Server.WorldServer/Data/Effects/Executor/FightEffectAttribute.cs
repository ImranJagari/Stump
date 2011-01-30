﻿// /*************************************************************************
//  *
//  *  Copyright (C) 2010 - 2011 Stump Team
//  *
//  *  This program is free software: you can redistribute it and/or modify
//  *  it under the terms of the GNU General Public License as published by
//  *  the Free Software Foundation, either version 3 of the License, or
//  *  (at your option) any later version.
//  *
//  *  This program is distributed in the hope that it will be useful,
//  *  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  *  GNU General Public License for more details.
//  *
//  *  You should have received a copy of the GNU General Public License
//  *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//  *
//  *************************************************************************/
using System;
using Stump.DofusProtocol.Enums;

namespace Stump.Server.WorldServer.Effects.Executor
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class FightEffectAttribute : Attribute
    {
        public FightEffectAttribute(EffectsEnum effect)
        {
            Effect = effect;
            Generate = true;
        }

        public FightEffectAttribute(EffectsEnum effect, bool generate)
        {
            Effect = effect;
            Generate = generate;
        }

        public EffectsEnum Effect
        {
            get;
            set;
        }

        public bool Generate
        {
            get;
            set;
        }
    }
}