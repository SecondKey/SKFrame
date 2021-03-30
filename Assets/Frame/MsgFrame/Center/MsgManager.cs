using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 消息管理器
/// 负责内置注册删除消息链表等方法供子类使用
/// </summary>
namespace GSFramework
{
    public class MsgManager
    {
        public string ManagerName { get; }
        public Dictionary<string, EventHandler> RootEventHandlers { get; set; }

        /// <summary>
        /// 全部的事件树
        /// </summary>
        public Dictionary<string, NotifiedObjectProxy> sendList = new Dictionary<string, NotifiedObjectProxy>();//存储注册的消息

        public MsgManager(string managerName)
        {
            ManagerName = managerName;
        }

        /// <summary>
        /// （批量）注册消息
        /// </summary>
        /// <param name="mono">注册消息的对象</param>
        /// <param name="msgs">注册的消息token列表</param>
        public void RegistMsg(INotifiedObject notifiedObject, params string[] msgs)
        {
            foreach (string msg in msgs)
            {
                RegistMsg(notifiedObject, msg);
            }
        }

        /// <summary>
        /// 注册一个消息
        /// </summary>
        /// <param name="id">消息的token</param>
        /// <param name="node">代理对象</param>
        public void RegistMsg(INotifiedObject notifiedObject, string msg)
        {
            NotifiedObjectProxy node = new NotifiedObjectProxy(notifiedObject);
            if (!sendList.ContainsKey(msg))
            {
                sendList.Add(msg, node);
            }
            else
            {
                NotifiedObjectProxy tmp = sendList[msg];
                tmp.AddNode(notifiedObject);
            }
        }

        /// <summary>
        /// 针对一个对象（批量）注销消息
        /// </summary>
        /// <param name="mono">注销消息的对象</param>
        /// <param name="msgs">注册的消息token列表</param>
        public void UnRegistMsg(NotifiedObject mono, params string[] msgs)
        {
            for (int i = 0; i < msgs.Length; i++)
            {
                UnRegistSingle(msgs[i], mono);
            }
        }

        /// <summary>
        /// 注销一个消息
        /// </summary>
        /// <param name="id">消息的token</param>
        /// <param name="data">注销的对象</param>
        public void UnRegistSingle(string token, NotifiedObject targetObject)
        {
            if (!sendList.ContainsKey(token))
            {
                ConditionLog.MsgLogError($"执行注销操作时，传入了没有注册的消息：{token}");
            }
            else
            {
                sendList[token].RemoveNode(targetObject);
            }
        }

        /// <summary>
        /// 接收到一个消息，根据
        /// </summary>
        /// <param name="tmpMsg"></param>
        public void LaunchEvent(EventArgs args)
        {
            MsgBase msg = args as MsgBase;
            switch (msg.SendMode)
            {
                case MsgSendMode.Auto:
                    if (!sendList.ContainsKey(msg.Token))
                    {
                        ConditionLog.MsgLog($"收到了未注册的自动消息：{msg.Token}，转为全局消息");
                        msg.SendMode = MsgSendMode.Global;
                        MsgTools.SendGlobalMsg();
                    }
                    else
                    {
                        sendList[msg.Token].HandleEvent(msg);
                    }
                    break;
                case MsgSendMode.Local:
                    if (!sendList.ContainsKey(msg.Token))
                    {
                        ConditionLog.MsgLogError($"收到了本地消息：{msg.Token}，但是该消息没有注册");
                    }
                    else
                    {
                        sendList[msg.Token].HandleEvent(msg);
                    }
                    break;
                case MsgSendMode.Global:
                    if (!sendList.ContainsKey(msg.Token))
                    {
                        ConditionLog.MsgLog($"收到了未注册的全局消息：{msg.Token}");
                    }
                    else
                    {
                        sendList[msg.Token].HandleEvent(msg);
                    }
                    break;
                default:
                    ConditionLog.MsgLogError("Manager收到了一个 未标注发送模式，或无法处理对应模式的Msg");
                    break;
            }
        }

        public void HandleRootEvent(EventArgs args)
        {
            throw new System.NotImplementedException();
        }
    }
}