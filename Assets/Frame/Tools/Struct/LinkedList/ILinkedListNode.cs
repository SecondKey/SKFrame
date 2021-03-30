using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface ILinkedListNode<T>
    {
        T Identify { get; }
        ILinkedListNode<T> NextNode { get; }

        Dictionary<string, EventHandler> Handlers { get; set; }

        void HandleEvent(EventArgs args);
        void AddNode(T nodeIdentify);
        void AddNode(ILinkedListNode<T> node);
        void AddNode(T nodeIdentify, T token);
        void AddNode(ILinkedListNode<T> node, T token);
        void RemoveNode(T identify);
    }

    public interface ILinkedListNode : ILinkedListNode<string> { }
}