using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GSFramework
{
    public class InjectionInitializer : IInitializer
    {
        public void Initialization(IInitializedObject initializedObject)
        {
            Type type = initializedObject.GetType();
            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => p.IsDefined(typeof(InjectAttribute), true)))
            {
                foreach (InjectAttribute attribute in (InjectAttribute[])property.GetCustomAttributes(typeof(InjectAttribute), true))
                {
                    if (Basic.CheckConditions(attribute.UseCondition))
                    {
                        if (string.IsNullOrEmpty(attribute.ParametersGetMode))
                        {
                            property.SetValue(initializedObject, property.GetPropertyInstence());
                        }
                        else
                        {
                            property.SetValue(initializedObject, property.GetPropertyInstence(attribute.ParametersGetMode));
                        }

                        break;
                    }
                }
            }

            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => p.IsDefined(typeof(InjectAttribute), true)))
            {
                List<object> parametersList = new List<object>();
                ParameterInfo[] parameters = method.GetParameters();

                string ParametersGetMode = (method.GetCustomAttribute(typeof(InjectAttribute)) as InjectAttribute).ParametersGetMode;
                if (string.IsNullOrEmpty(ParametersGetMode))
                {
                    string[] ParametersGetModes = ParametersGetMode.Split(',');
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parametersList.Add(parameters[i].GetParameterInstence(ParametersGetModes[i]));
                    }
                }
                else
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parametersList.Add(parameters[i].GetParameterInstence());
                    }
                }

                method.Invoke(initializedObject, parametersList.ToArray());
            }
        }
    }
}