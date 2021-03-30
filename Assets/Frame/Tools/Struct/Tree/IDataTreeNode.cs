using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IDataTreeNode<T> : ITreeNode<T>, IDataNode<T> { }

    public interface IDataTreeNode : IDataTreeNode<string> { }
}