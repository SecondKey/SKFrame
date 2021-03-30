using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IInitializer
    {
        void Initialization(IInitializedObject initializedObject);
    }
}