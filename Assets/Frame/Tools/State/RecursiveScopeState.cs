using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class RecursiveScopeState<T> : RecursiveState<T>, IScopeState<T>
    {

        public RecursiveScopeState(T defaultValue) :
            base(defaultValue)
        { }
        public RecursiveScopeState(T defaultValue, Action<T> changeStateEvent, Action<T> reductionStateEvent) :
            base(defaultValue, changeStateEvent, reductionStateEvent)
        { }


        public IDisposable SetScope(T nowValue)
        {
            ChangeState(nowValue);
            return this;
        }

        public void Dispose()
        {
            ReductionState();
        }
    }

}