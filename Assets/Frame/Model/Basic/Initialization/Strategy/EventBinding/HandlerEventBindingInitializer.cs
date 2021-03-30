using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GSFramework
{
    public class HandlerEventBindingInitializer : IInitializer
    {
        public void Initialization(IInitializedObject initializedObject)
        {
            Type type = initializedObject.GetType();
            string propertyName = "Handlers";
            Dictionary<string, EventHandler> tmpHandlers = new Dictionary<string, EventHandler>();
            foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => p.IsDefined(typeof(HandlerEventBindingAttribute), true)))
            {
                HandlerEventBindingAttribute attribute = Attribute.GetCustomAttribute(method, typeof(HandlerEventBindingAttribute)) as HandlerEventBindingAttribute;
                string key = attribute.EventToken;
                if (!string.IsNullOrEmpty(attribute.TargetProperty))
                {
                    propertyName = attribute.TargetProperty;
                }
                tmpHandlers.Add(key, (EventHandler)Delegate.CreateDelegate(typeof(EventHandler), initializedObject, method));
            }

            PropertyInfo property = type.GetProperty(propertyName);
            if (property != null)
            {
                property.SetValue(initializedObject, tmpHandlers);
            }
        }
    }
}