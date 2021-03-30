using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IDataContainer : IInitializedObject
    {
        void Load(string level, IState<string> state);

        Dictionary<string, GetterEvent> Getters { get; set; }
        object GetData(EventArgs args);
    }
}