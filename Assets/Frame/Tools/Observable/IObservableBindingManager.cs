using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IObservableManager
    {
        void BindingData(IObservableObject observableObject, IObserver observer);

        void UnbindData(IObservableObject observableObject, IObserver observer);

        void PropertyChange(IObservableObject observableObject, string propertyName, object value);
    }
}