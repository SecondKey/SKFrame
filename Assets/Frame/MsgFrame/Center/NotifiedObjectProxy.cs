using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GSFramework
{
    /// <summary>
    /// 链表式消息结构(消息节点的代理)
    /// </summary>
    public class NotifiedObjectProxy : ILinkedListNode<INotifiedObject>
    {
        public INotifiedObject Identify { get; }
        public ILinkedListNode<INotifiedObject> NextNode { get; }
        public Dictionary<string, EventHandler> Handlers { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public NotifiedObjectProxy(INotifiedObject notifiedObject)
        {
            Identify = notifiedObject;
            NextNode = null;
        }

        public void DebugList()
        {
            Debug.Log(Identify.ToString());
            if (NextNode != null)
            {
                (NextNode as NotifiedObjectProxy).DebugList();
            }
        }

        public void HandleEvent(EventArgs args)
        {
            throw new System.NotImplementedException();
        }

        public void AddNode(INotifiedObject nodeIdentify)
        {
            throw new System.NotImplementedException();
        }

        public void AddNode(ILinkedListNode<INotifiedObject> node)
        {
            throw new System.NotImplementedException();
        }

        public void AddNode(INotifiedObject nodeIdentify, INotifiedObject token)
        {
            throw new System.NotImplementedException();
        }

        public void AddNode(ILinkedListNode<INotifiedObject> node, INotifiedObject token)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveNode(INotifiedObject identify)
        {
            throw new System.NotImplementedException();
        }

        public void Initialization()
        {
            throw new System.NotImplementedException();
        }
    }
}
