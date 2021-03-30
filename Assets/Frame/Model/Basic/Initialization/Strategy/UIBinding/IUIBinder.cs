using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GSFramework
{
    public interface IUIBinder
    {
        void Binding(UIBehaviour uiComponent, UIBindingComponent uiToken, IUILogicalNode target);
        void PropertyChanged(string propertyName, object NewValue);
    }
}