using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GSFramework
{
    public interface IObservableObject
    {
        object GetData(string propertyName);
        void RaisePropertyChanged([CallerMemberName] string PropertyName = "");
        void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression);
    }
}