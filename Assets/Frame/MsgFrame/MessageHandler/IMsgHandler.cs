using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IMsgHandler
    {
        Dictionary<string, MsgEventHandler> MsgEventHandlers { get; set; }

        /// <summary>
        /// 开始新的操作
        /// </summary>
        /// <param name="msg">要发起的消息</param>
        /// <param name="operations">结束后要执行的操作</param>
        void StartOperation(MsgBase msg, params Action[] operations);

        /// <summary>
        /// 接收到消息,执行对应的事件
        /// </summary>
        /// <param name="tmpMsg">接收到的消息</param>
        void ProcessEvent(MsgBase msg);
    }
}