using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    [RequireComponent(typeof(InitIdent))]
    public abstract class InitializedGameObject : MonoBehaviour, IInitializedObject
    {
        public abstract void Initialization();
    }
}