using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GSFramework
{
    public static class InitializeManager
    {
        static Dictionary<string, IInitializer> initializers = new Dictionary<string, IInitializer>();

        public static void PerformInitialization(this IInitializedObject initialized)
        {
            GetInitializer(AppConst.Init_Inject).Initialization(initialized);
            foreach (InitializationAttribute attribute in Attribute.GetCustomAttributes(initialized.GetType(), typeof(InitializationAttribute)).Where(p => (p as InitializationAttribute).InitializeTime == AppConst.InitMode_Before))
            {
                GetInitializer(attribute.InitializeType).Initialization(initialized);
            }
            initialized.Initialization();
            foreach (InitializationAttribute attribute in Attribute.GetCustomAttributes(initialized.GetType(), typeof(InitializationAttribute)).Where(p => (p as InitializationAttribute).InitializeTime == AppConst.InitMode_After))
            {
                GetInitializer(attribute.InitializeType).Initialization(initialized);
            }
        }

        static IInitializer GetInitializer(string initializerName)
        {
            if (!initializers.ContainsKey(initializerName))
            {
                initializers.Add(initializerName, Basic.Instence.GetInstence<IInitializer>("", initializerName));
            }
            return initializers[initializerName];
        }
    }
}