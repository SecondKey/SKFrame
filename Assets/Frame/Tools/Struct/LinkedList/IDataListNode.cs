using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IDataNode<T> : ILinkedListNode<T>
    {
        Dictionary<string, GetterEvent> Getters { get; set; }
        object GetData(EventArgs args);
    }

    public interface IDataNode : IDataNode<string> { }
}