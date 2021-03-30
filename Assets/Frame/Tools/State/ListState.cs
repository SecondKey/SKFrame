using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class ListState<T> : IState<T>
    {
        public List<KeyValuePair<Action<T>, Action>> stateList;

        public T DefaultValue { get; private set; }
        public T NowValue { get; private set; }
        public bool IsSpecial { get { return nowStep != -1; } }

        int nowStep = -1;
        Action<T> ChangeStateEvent;
        Action<T> ReductionStateEvent;

        public Action<T> TmpChangeStateEvent;
        public Action<T> TmpReductionStateEvent;

        public ListState(T defalutValue)
        {
            DefaultValue = defalutValue;
            stateList = new List<KeyValuePair<Action<T>, Action>>();
        }

        public ListState(T defalutValue, Action<T> changeStateEvent, Action<T> reductionStateEvent, params KeyValuePair<Action<T>, Action>[] states) : this(defalutValue)
        {
            stateList = new List<KeyValuePair<Action<T>, Action>>(states);
            ChangeStateEvent = changeStateEvent;
            ReductionStateEvent = reductionStateEvent;
        }

        public void AddState(Action<T> changeStateAction)
        {
            stateList.Add(new KeyValuePair<Action<T>, Action>(changeStateAction, null));
        }

        public void AddState(Action<T> changeStateAction, Action reductionStateAction)
        {
            stateList.Add(new KeyValuePair<Action<T>, Action>(changeStateAction, reductionStateAction));
        }

        public void AddState(KeyValuePair<Action<T>, Action> state)
        {
            stateList.Add(state);
        }

        public void AddState(int index, KeyValuePair<Action<T>, Action> state)
        {
            stateList.Insert(index, state);
        }

        public void ChangeState(T nowValue)
        {
            if (!IsSpecial)
            {
                if (ChangeStateEvent != null)
                {
                    ChangeStateEvent.Invoke(nowValue);
                }
                NowValue = nowValue;
                nowStep = 0;
                if (stateList[nowStep].Key != null)
                {
                    stateList[nowStep].Key.Invoke(nowValue);
                }
            }
        }

        public void ReductionState()
        {
            if (stateList[nowStep].Value != null)
            {
                stateList[nowStep].Value.Invoke();
            }
            nowStep += 1;
            if (nowStep >= stateList.Count)
            {
                nowStep = -1;
                T tmpValue = NowValue;
                NowValue = DefaultValue;
                if (TmpReductionStateEvent != null)
                {
                    TmpReductionStateEvent.Invoke(tmpValue);
                }
                if (ReductionStateEvent != null)
                {
                    ReductionStateEvent.Invoke(tmpValue);
                }
            }
            else
            {
                if (stateList[nowStep].Key != null)
                {
                    stateList[nowStep].Key.Invoke(NowValue);
                }
            }
        }
    }
}