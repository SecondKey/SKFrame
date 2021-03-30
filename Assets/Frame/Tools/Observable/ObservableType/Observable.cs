using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GSFramework
{
    public enum CollectionChangedAction
    {
        /// <summary>
        /// 项已添加到集合中。
        /// </summary>
        Add = 0,
        /// <summary>
        /// 已从集合中删除项。
        /// </summary>
        Remove = 1,
        /// <summary>
        /// 已在集合中替换项。
        /// </summary>
        Replace = 2,
        /// <summary>
        /// 集合的内容已清除。
        /// </summary>
        Clear = 3
    }

    public interface INotifyCollectionChanged
    {
        event CollectionChangedEventHandler CollectionChanged;
    }

    public interface INotifyPropertyChanged
    {
        event EventHandler<string> PropertyChanged;
    }


    public delegate void CollectionChangedEventHandler(CollectionChangedEventArgs e);
    public class CollectionChangedEventArgs : EventArgs
    {
        public CollectionChangedEventArgs(string collection, CollectionChangedAction action) : base(collection)
        {
            if (action != CollectionChangedAction.Clear)
            {
                throw new Exception("传入了空的参数，但执行的不是清空集合的操作");
            }
            Action = action;
        }
        public CollectionChangedEventArgs(string collection, ICollection list, CollectionChangedAction action, object item, int index) : base(collection)
        {

            if (action == CollectionChangedAction.Add)
            {
                Action = action;
                Index = index;
                NewItem = item;
            }
            else if (action != CollectionChangedAction.Remove)
            {
                Action = action;
                Index = index;
                OldItem = item;
            }
            else
            {
                throw new Exception("使用了Add或Remove的事件参数，执行的不是Add或Remove方法");
            }
        }
        public CollectionChangedEventArgs(string collection, ICollection list, CollectionChangedAction action, object newItem, object oldItem, int index) : base(collection)
        {
            Action = action;
            NewItem = newItem;
            OldItem = oldItem;
            Index = index;
        }
        public CollectionChangedAction Action { get; }
        public ICollection list;
        public int Index { get; }
        public object OldItem { get; }
        public object NewItem { get; }
    }


}