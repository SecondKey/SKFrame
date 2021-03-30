using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IUILogicalNode : IUINode, ITreeNode
    {
        IObservableObject Model { get; set; }
        IUIViewModel DataContext { get; set; }

        GameObject Parent { get; }
        IUILogicalNode LogicParent { get; }

        IUINode FindLogicalTree(string objectName);
    }
}