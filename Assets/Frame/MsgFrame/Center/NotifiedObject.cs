using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 所有管理器的父类
    /// 内置接受消息的方法并强制重写
    /// </summary>
    public abstract class NotifiedObject : INotifiedObject, IInitializedObject
    {
        public abstract string MsgStstem { get; }


        public Dictionary<string, IMsgHandler> MsgHandlers { get; set; }
        #region 公用方法
        /// <summary>
        /// 接收消息
        /// 接收消息后对消息进行判断
        /// 找到对应的消息特性进行消息处理
        /// </summary>
        /// <param name="tmpMsg"></param>
        public void ProcessEvent(MsgBase msg)
        {
            foreach (var kv in MsgHandlers.Values)
            {
                if (kv.MsgEventHandlers.ContainsKey(msg.Token))
                {
                    kv.ProcessEvent(msg);
                    break;
                }
            }
            ConditionLog.MsgLogError($"查找{msg.Token}的执行方法时出错!在{GetType()}");
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        public void SendMsg(MsgBase msg, string handler = AppConst.MsgHandler_Common, params Action[] operations)//发送消息方法
        {
            msg.Source = this;
            if (MsgHandlers.ContainsKey(handler))
            {
                MsgHandlers[handler].StartOperation(msg, operations);
            }
            else
            {
                ConditionLog.MsgLogError($"没有找到指定的消息处理器{handler}在{GetType()}");
            }
        }


        /// <summary>
        /// 注册方法
        /// </summary>
        /// <param name="mono"></param>
        /// <param name="msgs"></param>
        public virtual void RegistSelf(params string[] msgs)
        {

        }
        /// <summary>
        /// 注销方法
        /// </summary>
        /// <param name="mono"></param>
        /// <param name="msgs"></param>
        public virtual void UnRegistSelf(params string[] msgs)
        {

        }

        public virtual void OnDestroy()
        {
            foreach (IMsgHandler MsgHandler in MsgHandlers.Values)
            {
                UnRegistSelf(new List<string>(MsgHandler.MsgEventHandlers.Keys).ToArray());
            }
        }
        #endregion


        public virtual void Initialization() { }
    }
}