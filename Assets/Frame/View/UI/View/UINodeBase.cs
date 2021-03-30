using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using System;

namespace GSFramework
{
    [Serializable]
    public class UINodeBase : MonoBehaviour, IUINode
    {
        [SerializeField]
        public string nodeToken = "";
        [SerializeField]
        public ObservableDictionary<string, bool> actionComponents;

        public string NodeToken { get { return nodeToken; } set { nodeToken = value; } }
    }

}

