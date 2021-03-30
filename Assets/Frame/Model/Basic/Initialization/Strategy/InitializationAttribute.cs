using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class InitializationAttribute : Attribute
    {
        public InitializationAttribute(string initializeType, string initializeTime = AppConst.InitMode_Before)
        {
            InitializeType = initializeType;
            InitializeTime = initializeTime;
        }

        public string InitializeType { get; }

        public string InitializeTime { get; }
        public string Parameter { get; set; }
    }
}