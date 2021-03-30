using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GSFramework
{
    public class UIViewModelBase : ObservableObject, IUIViewModel
    {
        Dictionary<string, UIBinderProxy> DataBindingList = new Dictionary<string, UIBinderProxy>();

        IObservableObject model;
        public IObservableObject Model
        {
            get { return model; }
            set
            {
                if (model != value)
                {
                    if (model != null)
                    {
                        Basic.Instence.GetSingleton<IObservableManager>().UnbindData(Model, this);
                    }
                    model = value;
                    Basic.Instence.GetSingleton<IObservableManager>().BindingData(Model, this);
                }
            }
        }

        public void BindingComponent(string token, IUIBinder binder)
        {
            if (!DataBindingList.ContainsKey(token))
            {
                DataBindingList.Add(token, new UIBinderProxy(binder));
            }
            else
            {
                DataBindingList[token].AddNode(binder);
            }
        }

        public override object GetData(string propertyName)
        {
            object tmpObject = base.GetData(propertyName);
            if (tmpObject == null)
            {
                tmpObject = model.GetData(propertyName);
            }
            return tmpObject;
        }

        public void PropertyChanged(string propertyName, object value)
        {
            if (DataBindingList.ContainsKey(propertyName))
            {
                DataBindingList[propertyName].HandleEvent(new PropertyChangedArgs(propertyName, value));
            }
        }

    }
}