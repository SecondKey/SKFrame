using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GSFramework
{
    public class ObservableObject : IObservableObject
    {
        public virtual object GetData(string propertyName)
        {
            PropertyInfo property = GetType().GetProperty(propertyName);
            if (property != null)
            {
                return property.GetValue(this);
            }
            else
            {
                return null;
            }
        }

        public virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            Basic.Instence.GetSingleton<IObservableManager>().PropertyChange(this, propertyName, GetType().GetProperty(propertyName).GetValue(this));
        }

        public virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            string propertyName = (propertyExpression.Body as MemberExpression).Member.Name;
            Basic.Instence.GetSingleton<IObservableManager>().PropertyChange(this, propertyName, propertyExpression.Compile().Invoke());
        }
    }
}