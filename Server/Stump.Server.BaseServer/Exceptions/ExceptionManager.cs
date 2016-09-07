﻿using Stump.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Stump.Core.Reflection;

namespace Stump.Server.BaseServer.Exceptions
{
    public class ExceptionManager : Singleton<ExceptionManager>
    {
        private readonly List<Exception> m_exceptions = new List<Exception>();

        public ReadOnlyCollection<Exception> Exceptions => m_exceptions.AsReadOnly();

        public void RegisterException(Exception ex)
        {
            //m_exceptions.Add(ex);
        }
    }
}