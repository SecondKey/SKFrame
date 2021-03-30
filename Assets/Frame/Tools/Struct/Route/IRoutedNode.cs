using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IRoutedNode<T> : ITreeNode<T>
    {
        void HandelEvent(RoutedEventArgs args);
    }
}