using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GSFramework
{
    public class TestButtonModel : ObservableObject
    {
        string testText = "测试文本";
        public string TestText
        {
            get { return testText; }
            set { TestText = value; RaisePropertyChanged(() => TestText); }
        }
    }
}