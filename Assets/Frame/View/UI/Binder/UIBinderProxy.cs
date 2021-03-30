using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GSFramework
{
    [Initialization(AppConst.Init_HandlerEventBinding)]
    public class UIBinderProxy : ILinkedListNode<IUIBinder>
    {
        public IUIBinder Identify { get; }
        public ILinkedListNode<IUIBinder> NextNode { get; private set; }
        public Dictionary<string, EventHandler> Handlers { get; set; }

        public UIBinderProxy(IUIBinder binder)
        {
            Identify = binder;
        }

        #region Handler
        [HandlerEventBinding("PropertyChanged")]
        void PropertyChanged(EventArgs args)
        {
            PropertyChangedArgs tmpArgs = args as PropertyChangedArgs;
            Identify.PropertyChanged(tmpArgs.PropertyName,tmpArgs.Value);
        }
        #endregion 

        public void AddNode(IUIBinder binder)
        {
            if (NextNode == null)
            {
                NextNode = new UIBinderProxy(binder);
            }
            else
            {
                NextNode.AddNode(binder);
            }
        }

        public void HandleEvent(EventArgs args)
        {
            Handlers[args.Token].Invoke(args);
        }

        public void RemoveNode(IUIBinder identify)
        {
            throw new System.NotImplementedException();
        }

        #region Useless
        public void AddNode(IUIBinder nodeIdentify, IUIBinder token) { throw new System.NotImplementedException(); }

        public void AddNode(ILinkedListNode<IUIBinder> node) { throw new System.NotImplementedException(); }

        public void AddNode(ILinkedListNode<IUIBinder> node, IUIBinder token) { throw new System.NotImplementedException(); }
        #endregion 
    }
}