using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GSFramework
{

    /// <summary>
    /// ObservableCollection的字典版
    /// </summary>
    public class ObservableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, INotifyCollectionChanged
    {
        public ObservableDictionary(string dictionaryName) : base()
        {
            DictionaryName = dictionaryName;
        }

        private string DictionaryName { get; set; }
        private int _index;
        public event CollectionChangedEventHandler CollectionChanged;

        public new KeyCollection Keys
        {
            get { return base.Keys; }
        }

        public new ValueCollection Values
        {
            get { return base.Values; }
        }

        public new int Count
        {
            get { return base.Count; }
        }

        public new TValue this[TKey key]
        {
            get { return this.GetValue(key); }
            set { this.SetValue(key, value); }
        }

        public TValue this[int index]
        {
            get { return this.GetIndexValue(index); }
            set { this.SetIndexValue(index, value); }
        }

        public new void Add(TKey key, TValue value)
        {
            base.Add(key, value);
            OnCollectionChanged(new CollectionChangedEventArgs(DictionaryName, (ICollection)this, CollectionChangedAction.Add, FindPair(key), _index));
        }

        public new bool Remove(TKey key)
        {
            var pair = this.FindPair(key);
            if (base.Remove(key))
            {
                OnCollectionChanged(new CollectionChangedEventArgs(DictionaryName, (ICollection)this, CollectionChangedAction.Remove, pair, _index));
                return true;
            }
            return false;
        }

        public new void Clear()
        {
            base.Clear();
            OnCollectionChanged(new CollectionChangedEventArgs(DictionaryName, CollectionChangedAction.Clear));
        }

        public int IndexOf(TKey key)
        {
            int index = 0;
            foreach (var item in this)
            {
                if (item.Key.Equals(key))
                {
                    return index;
                }
                index++;

            }
            return -1;
        }

        #region 私有方法
        private TValue GetIndexValue(int index)
        {
            if (index >= Count)
            {
                return this.ElementAt(index).Value;
            }
            return default;
        }


        private TValue GetValue(TKey key)
        {
            if (ContainsKey(key))
            {
                return base[key];
            }
            else
            {
                return default;
            }
        }

        private void SetIndexValue(int index, TValue value)
        {
            if (index < Count)
            {
                var pair = this.ElementAtOrDefault(index);
                SetValue(pair.Key, value);
            }
            else
            {
                throw new Exception();
            }
        }

        private void SetValue(TKey key, TValue value)
        {
            if (base.ContainsKey(key))
            {
                var pair = this.FindPair(key);
                int index = _index;
                base[key] = value;
                var newpair = this.FindPair(key);
                this.OnCollectionChanged(new CollectionChangedEventArgs(DictionaryName, (ICollection)this, CollectionChangedAction.Replace, newpair, pair, index));
            }
            else
            {
                this.Add(key, value);
            }
        }

        private KeyValuePair<TKey, TValue> FindPair(TKey key)
        {
            _index = 0;
            foreach (var item in this)
            {
                if (item.Key.Equals(key))
                {
                    return item;
                }
                _index++;
            }
            return default;
        }

        protected void OnCollectionChanged(CollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(e);
        }
        #endregion
    }
}