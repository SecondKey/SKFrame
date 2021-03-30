using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class ObjectArgs : EventArgs
    {
        public string ScriptType { get; }
        public string ScriptToken { get; }
        public string Group { get; }

        public ObjectArgs(string scriptType, string performer, string scriptToken, string group) : base("GetObject", performer)
        {
            ScriptType = scriptType;
            ScriptToken = scriptToken;
            Group = group;
        }
    }
}