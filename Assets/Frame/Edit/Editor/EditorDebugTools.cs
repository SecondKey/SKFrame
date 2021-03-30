using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace GSFramework
{
    public class EditorDebugTools
    {
        const string MsgPath = "工具/输出/消息框架";

        [MenuItem(MsgPath + "/Msg")]
        static void ShowMsg()
        {
            //MsgCenter.GetInstence().DebugAllMsg();
        }
    }
}