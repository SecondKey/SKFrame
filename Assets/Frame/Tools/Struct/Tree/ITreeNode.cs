using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface ITreeNode<T> : ILinkedListNode<T>
    {
        int Deep { get; }
        ITreeRoot Root { get; }
        Dictionary<T, ITreeNode<T>> NextCollection { get; }

        void RemoveDeep(int deep);
    }

    public interface ITreeNode : ITreeNode<string> { }
}