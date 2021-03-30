using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class UpdateManager : IUpdateManager
    {
        public void Update(IState<bool> updateState)
        {
            updateState.ReductionState();
        }
    }
}