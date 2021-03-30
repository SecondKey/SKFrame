using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 范围状态，使用using限制特殊状态的作用域
    /// 离开作用域将自动结束特殊状态，或手动退出特殊状态
    /// 在作用域中对状态进行任何修改，在离开作用域会立即还原，并执行退出方法
    /// </summary>
    public class ScopeState<T> : CommonState<T>, IScopeState<T>
    {
        public ScopeState(T defaultValue) : base(defaultValue) { }
        public ScopeState(T defaultValue, Action<T> changeStateEvent, Action<T> reductionStateEvent) : base(defaultValue, changeStateEvent, reductionStateEvent) { }

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