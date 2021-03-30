using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class DynamicDataContainer : IDynamicDataContainer
    {
        public string Identify => throw new System.NotImplementedException();

        public Dictionary<string, EventHandler> Handlers { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public Dictionary<string, GetterEvent> Getters { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void AddNode(string nodeIdentify)
        {
            throw new System.NotImplementedException();
        }

        public void AddNode(IDataNode node)
        {
            throw new System.NotImplementedException();
        }

        public void AddNode(string nodeIdentify, string token)
        {
            throw new System.NotImplementedException();
        }

        public void AddNode(IDataNode node, string token)
        {
            throw new System.NotImplementedException();
        }

        public object GetData(EventArgs args)
        {
            throw new System.NotImplementedException();
        }

        public void HandleEvent(EventArgs args)
        {
            throw new System.NotImplementedException();
        }

        public void Initialization()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveNode(string identify)
        {
            throw new System.NotImplementedException();
        }
    }
}