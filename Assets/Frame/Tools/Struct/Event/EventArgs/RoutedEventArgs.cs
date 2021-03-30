using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public enum RoutedStrategy
    {
        Bubble,
        Tunnel,
        ReverseBubble,
        ReverseTunnel,
        TopToBottom,
        BottomToTop,
    }

    public class RoutedEventArgs : EventArgs
    {
        /// <summary>
        /// 路由事件的路由标识
        /// </summary>
        RoutedStrategy Strategy { get; }

        /// <summary>
        /// 事件的发送源
        /// </summary>
        public object Source { get; }

        /// <summary>
        /// 确切的事件发起源
        /// </summary>
        public object OriginalSource { get; }

        public RoutedEventArgs(RoutedStrategy strategy, string token) : base(token)
        {
            Strategy = strategy;
        }

        public RoutedEventArgs(RoutedStrategy strategy, string token, string source) : base(token, source)
        {
            Strategy = strategy;
            OriginalSource = source;
        }

        public RoutedEventArgs(RoutedStrategy strategy, string token, string source, string originalSource) : base(token, source)
        {
            Strategy = strategy;
            OriginalSource = originalSource;
        }
    }
}