using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IDoubleLinkedList<T> : ILinkedListNode<T>
    {
        IDoubleLinkedList<T> PreviouNode { get; }
    }

    public interface IDoubleLinkedList : IDoubleLinkedList<string> { }
}