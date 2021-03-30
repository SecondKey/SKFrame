using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 所有消息的父类
/// 通过在子类中添加参数可以传递各种参数
/// </summary>
namespace GSFramework
{
    public class MsgBase : EventArgs
    {
        public MsgSendMode SendMode { get; set; }

        public object Source { get; set; }

        public MsgBase(string token, MsgSendMode sendMode) : base(token)
        {
            SendMode = sendMode;
        }
    }

    public class MsgBase<T> : MsgBase
    {
        public T Parameter { get; set; }
        public MsgBase(string token, MsgSendMode sendMode, T parameter) : base(token, sendMode)
        {
            Parameter = parameter;
        }
    }
}