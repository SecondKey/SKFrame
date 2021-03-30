using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false)]
    public class DefaultConstructorAttribute : Attribute
    {
        public string ParametersGetMode { get; set; }
    }
}