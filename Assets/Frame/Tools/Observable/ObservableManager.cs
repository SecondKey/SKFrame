using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class ObservableManager : IObservableManager
    {
        Dictionary<IObservableObject, List<IObserver>> ObserveList = new Dictionary<IObservableObject, List<IObserver>>();

        public void BindingData(IObservableObject observableObject, IObserver observer)
        {
            if (!ObserveList.ContainsKey(observableObject))
            {
                ObserveList.Add(observableObject, new List<IObserver>() { observer });
            }
            else if (!ObserveList[observableObject].Contains(observer))
            {
                ObserveList[observableObject].Add(observer);
            }
        }

        public void UnbindData(IObservableObject observableObject, IObserver observer)
        {
            if (ObserveList.ContainsKey(observableObject) && ObserveList[observableObject].Contains(observer))
            {
                ObserveList[observableObject].Remove(observer);
            }
        }

        public void PropertyChange(IObservableObject observableObject, string propertyName, object value)
        {
            if (ObserveList.ContainsKey(observableObject))
            {
                foreach (IObserver observer in ObserveList[observableObject])
                {
                    observer.PropertyChanged(propertyName, value);
                }
            }
        }
    }
}