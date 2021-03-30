using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GSFramework
{
    public class GetterEventBindingInitializer : IInitializer
    {

        public void Initialization(IInitializedObject initializedObject)
        {
            Type type = initializedObject.GetType();
            string propertyName = "Getters";
            Dictionary<string, GetterEvent> tmpHandlers = new Dictionary<string, GetterEvent>();
            foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => p.IsDefined(typeof(GetterEventBindingAttribute), true)))
            {
                GetterEventBindingAttribute attribute = Attribute.GetCustomAttribute(method, typeof(GetterEventBindingAttribute)) as GetterEventBindingAttribute;
                string key = attribute.EventToken;
                if (!string.IsNullOrEmpty(attribute.TargetProperty))
                {
                    propertyName = attribute.TargetProperty;
                }
                tmpHandlers.Add(key, (GetterEvent)Delegate.CreateDelegate(typeof(GetterEvent), initializedObject, method));
            }

            PropertyInfo property = type.GetProperty(propertyName);
            if (property != null)
            {
                property.SetValue(initializedObject, tmpHandlers);
            }
        }
    }
}