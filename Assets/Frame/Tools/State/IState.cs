using UnityEngine;
using System;
using System.Collections.Generic;

namespace GSFramework
{
    /// <summary>
    /// 标准的状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IState<T>
    {
        T DefaultValue { get; }
        T NowValue { get; }
        bool IsSpecial { get; }

        /// <summary>
        /// 标准的修改状态的方法
        /// </summary>
        /// <param name="nowValue">目标值</param>
        void ChangeState(T nowValue);
        /// <summary>
        /// 标准的还原状态的方法
        /// </summary>
        void ReductionState();
    }

    //public class RecursiveScopeState<T> : RecursiveState<T>, IDisposable { }
}