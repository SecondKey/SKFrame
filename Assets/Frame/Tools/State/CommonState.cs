using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{

    /// <summary>
    /// 通用的状态，使用ChangeState
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommonState<T> : IDynamicEventState<T>
    {
        public T DefaultValue { get; private set; }
        public T NowValue { get; private set; }
        public bool IsSpecial { get; private set; }

        #region Event
        event Action<T> ChangeStateEvent;
        event Action<T> ReductionStateEvent;

        public event Action<T> TmpChangeStateEvent;
        public event Action<T> TmpReductionStateEvent;

        #endregion
        public CommonState(T defaultValue)
        {
            this.DefaultValue = defaultValue;
        }

        public CommonState(T defaultValue, Action<T> changeStateEvent, Action<T> reductionStateEvent) : this(defaultValue)
        {
            this.ChangeStateEvent = changeStateEvent;
            this.ReductionStateEvent = reductionStateEvent;
        }

        public void ChangeState(T nowValue)
        {
            if (ChangeStateEvent != null)
            {
                ChangeStateEvent.Invoke(nowValue);
            }
            if (TmpChangeStateEvent != null)
            {
                TmpChangeStateEvent.Invoke(nowValue);
            }
            NowValue = nowValue;
            IsSpecial = true;
        }


        public void ChangeState(T nowValue, Action<T> tmpChangeStateEvent, Action<T> tmpReductionStateEvent)
        {

            this.TmpChangeStateEvent += tmpChangeStateEvent;
            this.TmpReductionStateEvent += tmpReductionStateEvent;
            ChangeState(nowValue);
        }

        public void SetState(T nowValue)
        {
            NowValue = nowValue;
            IsSpecial = true;
        }

        public void ReductionState()
        {
            if (ReductionStateEvent != null)
            {
                ReductionStateEvent.Invoke(NowValue);
            }
            if (TmpReductionStateEvent != null)
            {
                TmpReductionStateEvent.Invoke(NowValue);
            }
            TmpChangeStateEvent = null;
            TmpReductionStateEvent = null;
            NowValue = DefaultValue;
            IsSpecial = false;
        }
    }

}