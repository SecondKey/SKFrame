using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class TestState : MonoBehaviour
    {
        #region TestState
        RecursiveState<int> state = new RecursiveState<int>(0, (i) => { Debug.Log("Enter State"); }, (i) => { Debug.Log("Exit State"); });
        int num = 0;

        public void EnterState()
        {
            num += 1;
            state.ChangeState(num);
        }

        public void ShowState()
        {
            List<int> tmp = new List<int>();
            string s = "value list is :";
            foreach (int t in state.GetStateList(ref tmp))
            {

                s += +t + " ";
            }
            Debug.Log("now value is :" + state.NowValue);
            Debug.Log(s);
        }

        public void ExitState()
        {
            num -= 1;
            state.ReductionState();
        }
        #endregion
    }
}