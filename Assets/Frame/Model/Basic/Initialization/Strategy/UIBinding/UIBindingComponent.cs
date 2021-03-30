using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{

    [Serializable]
    public class AttributesToken
    {
        [SerializeField]
        public string attribute = "";
        [SerializeField]
        public string path = "";
    }
    [Serializable]
    public class UIBindingComponent : MonoBehaviour
    {
        [SerializeField]
        public string targetComponent = "";

        [SerializeField]
        public string componentToken = "";

        [SerializeField]
        public List<AttributesToken> AttributeTokens = new List<AttributesToken>();
    }
}