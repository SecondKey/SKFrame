using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    [Initialization(AppConst.Init_Inject)]
    [Initialization(AppConst.Init_UIBinding, AppConst.InitMode_After)]
    public abstract class UILogicNodeBase : InitializedGameObject, IUILogicalNode, IInitializedObject
    {
        public virtual IObservableObject Model { get { return DataContext.Model; } set { DataContext.Model = value; } }
        public virtual IUIViewModel DataContext { get { return LogicParent.DataContext; } set { } }

        public string NodeToken { get; set; }


        public GameObject Parent { get { return transform.parent.gameObject; } }
        public IUILogicalNode LogicParent { get; private set; }

        public virtual ITreeRoot Root { get { return LogicParent.Root; } }

        public int Deep { get; }
        public string Identify { get { return gameObject.name; } }
        public Dictionary<string, ITreeNode<string>> NextCollection { get; set; }
        public Dictionary<string, EventHandler> Handlers { get; set; }

        public void AddNode(string nodeIdentify)
        {
            throw new System.NotImplementedException();
        }

        public void AddNode(string nodeIdentify, string token)
        {
            throw new System.NotImplementedException();
        }

        public void AddNode(ILinkedListNode<string> node)
        {
            throw new System.NotImplementedException();
        }

        public void AddNode(ILinkedListNode<string> node, string token)
        {
            throw new System.NotImplementedException();
        }



        public IUINode FindLogicalTree(string objectName)
        {
            throw new System.NotImplementedException();
        }

        public GameObject GetNode()
        {
            throw new System.NotImplementedException();
        }

        public void HandleEvent(EventArgs args)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveDeep(int deep)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveNode(string identify)
        {
            throw new System.NotImplementedException();
        }

        public override void Initialization() { }

        #region Useless
        public ILinkedListNode<string> NextNode => throw new System.NotImplementedException();
        #endregion
    }
}