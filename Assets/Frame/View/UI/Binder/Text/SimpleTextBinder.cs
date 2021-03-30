using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GSFramework
{
    public class SimpleTextBinder : IUIBinder
    {
        Text text;

        public void Binding(UIBehaviour uiComponent, UIBindingComponent uiToken, IUILogicalNode target)
        {
            text = uiComponent as Text;
            string path = uiComponent.name;
            if (uiToken.AttributeTokens.Where(p => p.attribute == "Text").Count() > 0)
            {
                path = uiToken.AttributeTokens.Where(p => p.attribute == "Text").First().path;
            }
            else if (!string.IsNullOrEmpty(uiToken.componentToken))
            {
                path = uiToken.componentToken;
            }
            else if (!string.IsNullOrEmpty(uiToken.gameObject.GetComponent<IUINode>().NodeToken))
            {
                target.DataContext.BindingComponent(uiToken.gameObject.GetComponent<IUINode>().NodeToken, this);
            }
            target.DataContext.BindingComponent(path, this);
            text.text = target.DataContext.GetData(path) as string;
        }

        public void PropertyChanged(string propertyName, object NewValue)
        {
            text.text = NewValue as string;
        }
    }
}