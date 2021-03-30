using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public abstract class BasicManagerBase : IDataNode, IInitializedObject
    {
        [Inject(ParametersGetMode = AppConst.Injection_Additional)]
        public string Identify { get; set; }
        public ILinkedListNode<string> NextNode { get; private set; }
        public Dictionary<string, EventHandler> Handlers { get; set; }
        public Dictionary<string, GetterEvent> Getters { get; set; }

        public void HandleEvent(EventArgs args)
        {
            if (string.IsNullOrEmpty(args.Performer as string) || args.Performer as string == Identify)
            {
                Handlers[args.Token].Invoke(args);
            }
            if (args.Performer as string == Identify || args.Handled == true)
            {
                return;
            }
            if (NextNode != null)
            {
                NextNode.HandleEvent(args);
            }
        }

        public object GetData(EventArgs args)
        {
            object tmpObject = null;
            if (string.IsNullOrEmpty(args.Performer as string) || args.Performer as string == Identify)
            {
                tmpObject = Getters[args.Token].Invoke(args);
                if (tmpObject != null || args.Performer as string == Identify)
                {
                    return tmpObject;
                }
            }
            if (NextNode != null)
            {
                return (NextNode as IDataNode).GetData(args);
            }
            return null;
        }


        public void AddNode(ILinkedListNode<string> node)
        {
            if (NextNode == null)
            {
                NextNode = node;
            }
            else
            {
                NextNode.AddNode(node);
            }
        }

        public void RemoveNode(string identify)
        {
            if (NextNode.Identify == identify)
            {
                NextNode = null;
            }
            else
            {
                NextNode.RemoveNode(identify);
            }
        }

        #region IInitializedObject Members
        public virtual void Initialization() { }
        #endregion 

        #region Useless
        public void AddNode(string nodeIdentify)
        {
            throw new NotImplementedException();

        }

        public void AddNode(string nodeIdentify, string token)
        {
            throw new NotImplementedException();
        }

        public void AddNode(ILinkedListNode<string> node, string token)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}