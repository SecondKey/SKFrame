using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace GSFramework
{
    public class TestObservableDictionary : MonoBehaviour
    {
        #region TestObservableDictionary
        int i = 0;
        ObservableDictionary<string, string> test = new ObservableDictionary<string, string>("test");
        private void Start()
        {
            test.CollectionChanged += (CollectionChangedEventArgs e) => { Debug.Log(e.Token + " " + e.NewItem + " " + e.Index); };
        }
        public void TestAdd()
        {
            test.Add(i.ToString(), "test");
            i += 1;
        }
        #endregion
    }
}