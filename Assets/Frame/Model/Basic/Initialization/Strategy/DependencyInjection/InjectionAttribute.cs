using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class InjectAttribute : Attribute
    {
        public string ParametersGetMode { get; set; }

        public string UseCondition { get; set; }
    }
}