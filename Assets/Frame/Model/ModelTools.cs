using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.ComponentModel;
using System.Linq;

namespace GSFramework
{
    public static class BasicTools
    {
        #region Scripts
        public static object GetParameterInstence(this ParameterInfo parameter, string injectionType = "")
        {
            return GetParameterInstence(parameter.ParameterType, parameter.Name, injectionType);
        }

        public static object GetPropertyInstence(this PropertyInfo parameter, string injectionType = "")
        {
            return GetParameterInstence(parameter.PropertyType, parameter.Name, injectionType);
        }

        public static object GetParameterInstence(this Type type, string name, string injectionType = "")
        {

            if (injectionType == "")
            {
                if (type.IsPrimitive)
                {
                    return Activator.CreateInstance(type);
                }
                else if (type == typeof(string))
                {
                    return "";
                }
                else
                {
                    return Basic.Instence.GetInstence(type);
                }
            }
            else
            {
                switch (injectionType)
                {
                    case AppConst.Injection_Static:
                        return injectionType.Replace("Static_", "").ConvertToSimpleType(type);
                    case AppConst.Injection_Additional:
                        foreach (var tar in Basic.Instence.CreateInstenceState.GetBottomToTop())
                        {
                            if (tar is Dictionary<string, object>)
                            {
                                Dictionary<string, object> dic = tar as Dictionary<string, object>;
                                if (dic.ContainsKey(name))
                                {
                                    return dic[name];
                                }
                            }
                        }
                        return null;
                    case AppConst.Injection_InternalData:
                        return Basic.Instence.GetSingleton<IGameData>().GetData(name);
                    case AppConst.Injection_ExternalData:
                        return Basic.Instence.GetData(name);
                    case AppConst.Injection_DynamicData:
                        return Basic.Instence.GetDynamicData(name);
                    case AppConst.Injection_NewInstence:
                        return Basic.Instence.GetInstence(type, "", name);
                    case AppConst.Injection_NewGameObject:
                        return Basic.Instence.GetNewGameObject(name);
                    case AppConst.Injection_GameObject:
                        return Basic.Instence.GetGameObject(name);
                    default:
                        return injectionType.ConvertToSimpleType(type);
                }
            }
        }

        public static object CreateInstence(this string type)
        {
            Type t = Type.GetType(type);
            var ctorArray = t.GetConstructors();
            List<object> parametersList = new List<object>();
            ConstructorInfo ctor = ctorArray.Where(a => a.IsDefined(typeof(DefaultConstructorAttribute), true)).FirstOrDefault();
            if (ctor == null)
            {
                ctor = ctorArray.OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();
                foreach (ParameterInfo parameter in ctor.GetParameters())
                {
                    parametersList.Add(GetParameterInstence(parameter));
                }
            }
            else
            {
                string parametersGetMode = (Attribute.GetCustomAttribute(ctor, typeof(DefaultConstructorAttribute)) as DefaultConstructorAttribute).ParametersGetMode;
                ParameterInfo[] parameters = ctor.GetParameters();
                if (!string.IsNullOrEmpty(parametersGetMode))
                {
                    string[] parametersGetModes = parametersGetMode.Split(',');
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parametersList.Add(GetParameterInstence(parameters[i], parametersGetModes[i]));
                    }
                }
                else
                {
                    foreach (ParameterInfo parameter in ctor.GetParameters())
                    {
                        parametersList.Add(GetParameterInstence(parameter));
                    }
                }
            }
            object instence = Activator.CreateInstance(t, parametersList.ToArray());
            if (instence is IInitializedObject)
            {
                (instence as IInitializedObject).PerformInitialization();
            }
            return instence;
        }

        public static object CreateInstence(this Type type)
        {
            var ctorArray = type.GetConstructors();
            List<object> parametersList = new List<object>();
            ConstructorInfo ctor = ctorArray.Where(a => a.IsDefined(typeof(DefaultConstructorAttribute), true)).FirstOrDefault();
            if (ctor == null)
            {
                ctor = ctorArray.OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();
                foreach (ParameterInfo parameter in ctor.GetParameters())
                {
                    parametersList.Add(GetParameterInstence(parameter));
                }
            }
            else
            {
                string parametersGetMode = (Attribute.GetCustomAttribute(ctor, typeof(DefaultConstructorAttribute)) as DefaultConstructorAttribute).ParametersGetMode;
                ParameterInfo[] parameters = ctor.GetParameters();
                if (!string.IsNullOrEmpty(parametersGetMode))
                {
                    string[] parametersGetModes = parametersGetMode.Split(',');
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parametersList.Add(GetParameterInstence(parameters[i], parametersGetModes[i]));
                    }
                }
                else
                {
                    foreach (ParameterInfo parameter in ctor.GetParameters())
                    {
                        parametersList.Add(GetParameterInstence(parameter));
                    }
                }
            }
            object instence = Activator.CreateInstance(type, parametersList.ToArray());
            if (instence is IInitializedObject)
            {
                (instence as IInitializedObject).PerformInitialization();
            }
            return instence;
        }
        #endregion
    }
}