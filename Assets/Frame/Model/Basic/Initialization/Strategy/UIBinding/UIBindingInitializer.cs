using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Linq;

namespace GSFramework
{
    public class UIBindingInitializer : IInitializer
    {
        Dictionary<string, IUIBinder> binders = new Dictionary<string, IUIBinder>();

        public void Initialization(IInitializedObject initializedObject)
        {
            MonoBehaviour targetObject = (MonoBehaviour)initializedObject;
            foreach (GameObject g in targetObject.gameObject.GetInsideUI())
            {
                IUINode uiNode = g.GetComponent<IUINode>();
                if (uiNode != null)
                {
                    UIBindingComponent[] components = g.GetComponents<UIBindingComponent>();
                    foreach (UIBehaviour ui in g.GetComponents<UIBehaviour>())
                    {
                        UIBindingComponent[] targetBindingComponent = components.Where(p => p.targetComponent == ui.GetType().Name).ToArray();
                        if (targetBindingComponent.Length > 0)
                        {
                            GetBinder(ui.GetType().Name).Binding(ui, targetBindingComponent.First(), targetObject as IUILogicalNode);
                        }
                    }
                }
            }
        }

        public IUIBinder GetBinder(string uiComponent)
        {
            if (!binders.ContainsKey(uiComponent))
            {
                binders.Add(uiComponent, Basic.Instence.GetInstence<IUIBinder>("", uiComponent));
            }
            return binders[uiComponent];
        }

    }
}