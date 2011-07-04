using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using NLog;
using Stump.Core.Reflection;

namespace Stump.Server.BaseServer.Initialization
{
    public class InitializationManager : Singleton<InitializationManager>
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly List<Type> m_initializedTypes = new List<Type>();
        private readonly Dictionary<Type, List<InitializationMethod>> m_dependances = new Dictionary<Type, List<InitializationMethod>>();
        private readonly Dictionary<InitializationPass, List<InitializationMethod>> m_initializer =
            new Dictionary<InitializationPass, List<InitializationMethod>>();

        private InitializationManager()
        {
            foreach (InitializationPass pass in Enum.GetValues(typeof(InitializationPass)))   
            {
                m_initializer.Add(pass, new List<InitializationMethod>());
            }
        }

        public void AddAssemblies(IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
                AddAssembly(assembly);
        }

        public void AddAssembly(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
                {
                    var attribute = method.GetCustomAttribute<InitializationAttribute>();

                    if (attribute == null)
                        continue;

                    if (type.IsGenericType)
                        throw new Exception("Initialization method is within a generic type.");

                    if (!method.IsPublic)
                        throw new Exception("Initialization method must be public.");

                    if (method.IsGenericMethod)
                        throw new Exception("Initialization method must not be generic.");

                    if (method.ReturnType != typeof(void))
                        throw new Exception("Invalid initialization method return type.");

                    if (method.GetParameters().Length != 0)
                        throw new Exception("Invalid initialization cannot have parameters");

                    if (!m_initializer.ContainsKey(attribute.Pass))
                        m_initializer.Add(attribute.Pass, new List<InitializationMethod>());

                    var initializationMethod = new InitializationMethod(attribute, method);

                    
                    if (method.IsStatic)
                    {
                        initializationMethod.Caller = null; // null because it's static
                    }
                    // a bit ugly to get the instance in case of a singleton class
                    else if (type.IsDerivedFromGenericType(typeof(Singleton<>)))
                    {
                        var instanceProp = type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                        initializationMethod.Caller = instanceProp.GetValue(null, new object[0]);
                    }
                    else
                    {
                        throw new Exception("Method have to be static or class must inherit Singleton<>");
                    }

                    m_initializer[attribute.Pass].Add(initializationMethod);
                }
            }
        }

        private void ExecuteInitializationMethod(InitializationMethod method)
        {
            if (method.Initialized)
                return;

            if (method.Attribute.Dependance != null &&
                !m_initializedTypes.Contains(method.Attribute.Dependance))
            {
                if (!m_dependances.ContainsKey(method.Attribute.Dependance))
                    m_dependances.Add(method.Attribute.Dependance, new List<InitializationMethod>());

                m_dependances[method.Attribute.Dependance].Add(method);
            }
            else
            {
                if (!string.IsNullOrEmpty (method.Attribute.Name))
                    logger.Info(string.Format("Initialize '{0}'", method.Attribute.Name));
                else
                {
                    logger.Info(string.Format("Initialize '{0}'", method.Method.DeclaringType.Name));
                }

                method.Method.Invoke(method.Caller, new object[0]);

                method.Initialized = true;
                m_initializedTypes.Add(method.Method.DeclaringType);

                if (m_dependances.ContainsKey(method.Method.DeclaringType))
                {
                    foreach (var dependance in m_dependances[method.Method.DeclaringType])
                    {
                        ExecuteInitializationMethod(dependance);
                    }

                    m_dependances.Remove(method.Method.DeclaringType);
                }
            }
        }

        public void InitializeAll()
        {
            foreach (InitializationPass pass in Enum.GetValues(typeof(InitializationPass)))
            {
                foreach (var init in m_initializer[pass])
                {
                    ExecuteInitializationMethod(init);
                }

                m_initializer[pass].Clear();
            }
        }
    }
}