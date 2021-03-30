using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class GetterEventBindingAttribute : Attribute
    {
        public string EventToken { get; }
        public string TargetProperty { get; }
        public GetterEventBindingAttribute(string eventToken)
        {
            EventToken = eventToken;
        }
    }
}