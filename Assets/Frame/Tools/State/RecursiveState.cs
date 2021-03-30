using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class RecursiveState<T> : IDynamicEventState<T>
    {
        protected RecursiveState<T> nextState;

        public T DefaultValue { get; set; }
        public T NowValue { get { return nextState == null ? DefaultValue : nextState.NowValue; } }
        public bool IsSpecial { get { return nextState != null; } }
        public bool IsTop { get; set; } = true;

        public event Action<T> TmpChangeStateEvent;
        public event Action<T> TmpReductionStateEvent;

        public RecursiveState(T defaultValue)
        {
            DefaultValue = defaultValue;
        }

        public RecursiveState(T defaultValue, Action<T> changeStateEvent, Action<T> reductionStateEvent) : this(defaultValue)
        {
            TmpChangeStateEvent += changeStateEvent;
            TmpReductionStateEvent += reductionStateEvent;
        }

        public void ChangeState(T nowValue)
        {
            if ((IsTop || nextState == null) && TmpChangeStateEvent != null)
            {
                TmpChangeStateEvent.Invoke(nowValue);
            }
            if (nextState == null)
            {
                nextState = new RecursiveState<T>(nowValue);
                nextState.IsTop = false;
            }
            else
            {
                nextState.ChangeState(nowValue);
            }
        }

        public void ChangeState(T nowValue, Action<T> changeStateEvent, Action<T> reductionStateEvent)
        {
            if ((IsTop || nextState == null) && TmpChangeStateEvent != null)
            {
                TmpChangeStateEvent.Invoke(nowValue);
            }
            if (nextState == null)
            {
                nextState = new RecursiveState<T>(nowValue, changeStateEvent, reductionStateEvent);
                nextState.IsTop = false;
            }
            else
            {
                nextState.ChangeState(nowValue);
            }
        }

        public void SetState(T nowValue)
        {
            if (nextState == null)
            {
                DefaultValue = nowValue;
            }
            else
            {
                nextState.SetState(nowValue);
            }
        }

        public void ReductionState()
        {
            if (nextState != null && nextState.nextState == null)
            {
                if (nextState.TmpChangeStateEvent != null)
                {
                    nextState.TmpReductionStateEvent.Invoke(nextState.NowValue);
                }
                nextState = null;
            }
            else if (nextState != null)
            {
                nextState.ReductionState();
            }
        }

        public List<T> GetStateList(ref List<T> list)
        {
            list.Add(DefaultValue);
            if (nextState != null)
            {
                nextState.GetStateList(ref list);
            }
            return list;
        }

        public IEnumerable GetBottomToTop()
        {
            if (nextState != null)
            {
                foreach (T value in nextState.GetBottomToTop())
                {
                    yield return value;
                }
            }
            yield return DefaultValue;
        }
    }

}