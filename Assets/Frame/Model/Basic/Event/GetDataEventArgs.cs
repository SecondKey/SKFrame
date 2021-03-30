using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class GetDataEventArgs : EventArgs<string[]>
    {
        public string GetMode { get; }
        public GetDataEventArgs(string token, string getMode, string[] parameter, object performer = null) : base(token, parameter, performer)
        {
            GetMode = getMode;
        }
    }
}