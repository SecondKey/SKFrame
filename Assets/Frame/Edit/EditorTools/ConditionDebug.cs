using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 可选择的输出测试
    /// </summary>
    public class ConditionLog
    {
        #region Message
        public static bool MsgDebug;

        public static void MsgLog(object text)
        {
            if (MsgDebug)
            {
                Debug.Log("Message:" + text);
            }
        }
        public static void MsgLogError(object text)
        {
            Debug.LogError("Message错误:" + text);
        }
        #endregion

        #region Script
        public static bool ScriptDebug;

        public static void ScriptLog(object text)
        {
            if (ScriptDebug)
            {
                Debug.Log("Message:" + text);
            }
        }
        public static void ScriptLogError(object text)
        {
            Debug.LogError("Message错误:" + text);
        }
        #endregion 

        #region IO
        public static bool BasicDebug;

        public static void BasicLog(object text)
        {
            if (BasicDebug)
            {
                Debug.Log("Basic:" + text);
            }
        }
        public static void BasicLogError(object text)
        {
            Debug.LogError("IO错误:" + text);
        }
        #endregion

        #region Tools
        public static bool ToolsDebug;

        public static void ToolsLog(object text)
        {
            if (ToolsDebug)
            {
                Debug.Log("Tools:" + text);
            }
        }
        public static void ToolsLogError(object text)
        {
            Debug.LogError("Tools错误:" + text);
        }
        #endregion

        #region UI
        public static bool UIDebug;

        public static void UILog(object text)
        {
            if (UIDebug)
            {
                Debug.Log("UI:" + text);
            }
        }
        public static void UILogError(object text)
        {
            Debug.LogError("UI错误:" + text);
        }
        #endregion

        #region Running
        public static bool RunningDebug;

        public static void RunningLog(object text)
        {
            if (RunningDebug)
            {
                Debug.Log("Running:" + text);
            }
        }

        public static void RunningLogError(object text)
        {
            Debug.LogError("Running错误:" + text);
        }
        #endregion
    }
}