using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class PropertyChangedArgs : EventArgs
    {
        public string PropertyName { get; }
        public object Value { get; }
        public PropertyChangedArgs(string propertyName, object value) : base("PropertyChanged", null)
        {
            PropertyName = propertyName;
            Value = value;
        }
    }
}