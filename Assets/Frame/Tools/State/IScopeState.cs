using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IScopeState<T> : IDynamicEventState<T>, IDisposable
    {
        IDisposable SetScope(T nowValue);
    }
}