using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class WaitMsgHandler : IMsgHandler
    {
        public Dictionary<string, MsgEventHandler> MsgEventHandlers { get; set; }

        /// <summary>
        /// 等待队列
        /// </summary>
        public Dictionary<string, Dictionary<NotifiedObject, bool>> waitQueue = new Dictionary<string, Dictionary<NotifiedObject, bool>>();
        /// <summary>
        /// 等待队列是否完整,防止某个操作在发送完消息前就执行完
        /// </summary>
        public Dictionary<string, bool> waitQueueComplete = new Dictionary<string, bool>();
        /// <summary>
        /// 操作队列,在创建等待时动态添加
        /// </summary>
        public Dictionary<string, Action> nextExecute = new Dictionary<string, Action>();


        public void StartOperation(MsgBase msg, params Action[] operations)
        {
            #region 添加下一个操作
            if (operations != null)
            {
                if (!nextExecute.ContainsKey(msg.Token))
                {
                    nextExecute.Add(msg.Token, operations[0]);
                }
                else
                {
                    nextExecute[msg.Token] = operations[0];
                }
            }
            else
            {
                Debug.LogError("没有执行队列");
                return;
            }
            #endregion

            #region 添加到等待队列
            if (!waitQueue.ContainsKey(msg.Token))
            {
                waitQueue.Add(msg.Token, new Dictionary<NotifiedObject, bool>());
            }
            waitQueueComplete.Add(msg.Token, false);

            #endregion

            waitQueueComplete[msg.Token] = true;

            CheakQueue(msg.Token);
        }



        public void ProcessEvent(MsgBase msg)
        {
            MsgWaitStruce systemParameters = (msg as MsgWait).Parameter;

            if (systemParameters.complete == false)
            {
                if (waitQueue.ContainsKey(systemParameters.targetMsg))
                {
                    if (waitQueue[systemParameters.targetMsg].ContainsKey(systemParameters.waitObject))
                    {
                        Debug.LogError("AttributeWait,相同的对象传递了两次False，队列为:" + systemParameters.targetMsg);
                    }
                    else
                    {
                        waitQueue[systemParameters.targetMsg].Add(systemParameters.waitObject, false);
                    }
                }
                else
                {
                    Debug.LogError("没有指定队列:" + systemParameters.targetMsg);
                }
            }
            else
            {
                waitQueue[systemParameters.targetMsg][systemParameters.waitObject] = true;
                CheakQueue(systemParameters.targetMsg);
            }
        }

        /// <summary>
        /// 检查一个队列是否全部完成
        /// </summary>
        /// <param name="msg">指定的等待队列</param>
        public void CheakQueue(string msg)
        {
            if (waitQueueComplete[msg] && !waitQueue[msg].ContainsValue(false))
            {
                waitQueue.Remove(msg);
                waitQueueComplete.Remove(msg);

                if (nextExecute.ContainsKey(msg))
                {
                    Action tmp = nextExecute[msg];
                    nextExecute.Remove(msg);
                    tmp();
                }
            }
        }

    }
}