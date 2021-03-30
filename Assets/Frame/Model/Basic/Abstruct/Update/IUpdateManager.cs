using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IUpdateManager
    {
        void Update(IState<bool> updateState);
    }
}