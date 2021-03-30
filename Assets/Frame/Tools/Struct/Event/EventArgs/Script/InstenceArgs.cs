using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class InstenceArgs : EventArgs
    {
        public string ScriptType { get; }
        public string ScriptToken { get; }

        public InstenceArgs(string scriptType, string performer, string scriptToken) : base("GetInstence", performer)
        {
            ScriptType = scriptType;
            ScriptToken = scriptToken;
        }
    }
}