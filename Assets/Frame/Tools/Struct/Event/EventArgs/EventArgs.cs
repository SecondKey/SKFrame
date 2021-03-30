using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class EventArgs
    {
        /// <summary>
        /// 执行事件的标识
        /// </summary>
        public virtual string Token { get; }

        /// <summary>
        /// 指定执行人
        /// </summary>
        public virtual object Performer { get; }

        /// <summary>
        /// 事件是否已经处理
        /// </summary>
        public bool Handled { get; set; }

        /// <summary>
        /// 初始化EventArgs实例
        /// </summary>
        /// <param name="token">事件的标识</param>
        /// <param name="source">事件的发送源</param>
        /// <param name="parameters">事件的参数</param>
        public EventArgs(string token, object performer = null)
        {
            Token = token;
            Performer = performer;
        }
    }

    public class EventArgs<T> : EventArgs
    {
        public virtual T Parameter { get; }

        public EventArgs(string token, T parameter, object performer = null) : base(token, performer)
        {
            Parameter = parameter;
        }
    }
}