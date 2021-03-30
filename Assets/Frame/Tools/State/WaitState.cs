using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class WaitState<T>
    {
        protected WaitState<T> nextState;
        private bool isFirst;
        private bool addAlready;

        public T StateToken { get; }
        public bool IsSpecial { get { return nextState != null; } }

        public event Action MainChange;
        public event Action<T> EachChange;
        public event Action<T> EachReduction;
        public event Action MainReduction;

        public WaitState(T stateToken, bool isFirst = false)
        {
            this.isFirst = isFirst;
            StateToken = stateToken;
        }

        public void ChangeState(T token)
        {
            if (isFirst)
            {
                if (nextState == null)
                {
                    if (MainChange != null)
                    {
                        MainChange.Invoke();
                    }
                }
                if (EachChange != null)
                {
                    EachChange.Invoke(token);
                }
            }

            if (nextState == null)
            {
                nextState = new WaitState<T>(token, false);
            }
            else
            {
                nextState.ChangeState(token);
            }
        }

        public void ReductionState(T token)
        {
            if (nextState.StateToken as object == token as object)
            {
                nextState = nextState.nextState;

                if (isFirst)
                {
                    if (EachReduction != null)
                    {
                        EachReduction.Invoke(token);
                    }
                    if (nextState == null && addAlready)
                    {
                        MainReduction.Invoke();
                    }
                }
            }
            else
            {
                nextState.ReductionState(token);
            }
        }

        public void AddAlready()
        {
            addAlready = true;
            if (nextState == null)
            {
                MainReduction.Invoke();
            }
        }
    }
}