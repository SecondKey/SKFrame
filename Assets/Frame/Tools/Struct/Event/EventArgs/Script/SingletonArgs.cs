using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class SingletonArgs : EventArgs
    {
        public string ScriptType { get; }
        public string ScriptToken { get; }
        public string SingletonToken { get; }

        public SingletonArgs(string scriptType, string performer, string scriptToken, string singletonToken) : base("GetSingleton", performer)
        {
            ScriptType = scriptType;
            ScriptToken = scriptToken;
            SingletonToken = singletonToken;
        }
    }
}