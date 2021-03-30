using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class CommonMsgHandler : IMsgHandler
    {
        /// <summary>
        /// 所有的操作
        /// </summary>
        public Dictionary<string, MsgEventHandler> MsgEventHandlers { get; set; }

        public void ProcessEvent(MsgBase msg)
        {
            if (MsgEventHandlers.ContainsKey(msg.Token))
            {
                MsgEventHandlers[msg.Token].Invoke(msg);
            }
            else
            {
                ConditionLog.MsgLog($"Common中没有注册用于处理{msg.Token}的方法");
            }
        }

        public void StartOperation(MsgBase msg, params Action[] operations)
        {
            ConditionLog.MsgLog($"由Common发送了一个{msg.Token}消息");
        }
    }
}