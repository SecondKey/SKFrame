using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IDynamicEventState<T> : IState<T>
    {
        event Action<T> TmpChangeStateEvent;
        event Action<T> TmpReductionStateEvent;
        void SetState(T nowValue);
    }
}