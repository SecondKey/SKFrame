using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 回传等待消息
/// </summary>
namespace GSFramework
{
    public struct MsgWaitStruce
    {
        public string targetMsg;

        public NotifiedObject waitObject;

        public bool complete;
    }

    public class MsgWait : MsgBase<MsgWaitStruce>
    {
        public MsgWait(string token, MsgSendMode sendMode, MsgWaitStruce systemParameter) : base(token, sendMode, systemParameter) { }
    }
}