using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GSFramework
{
    public class ChangeStaticVar
    {
        #region 变量
        const string MsgPath = "工具/调试/输出/Msg";
        const string ScriptPath = "工具/调试/输出/Script";
        const string BasicPath = "工具/调试/输出/Basic";
        const string ToolsPath = "工具/调试/输出/Tools";
        const string UIPath = "工具/调试/输出/UI";
        const string RunningPath = "工具/调试/输出/Running";

        const string DataLoadPath = "工具/加载/数据";
        const string PackageLoadPath = "工具/加载/资源包";
        const string StorageLoadPath = "工具/加载/存档";
        #endregion

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void SetVar()
        {
            ConditionLog.MsgDebug = Menu.GetChecked(MsgPath);
            ConditionLog.ScriptDebug = Menu.GetChecked(ScriptPath);
            ConditionLog.BasicDebug = Menu.GetChecked(BasicPath);
            ConditionLog.ToolsDebug = Menu.GetChecked(ToolsPath);
            ConditionLog.UIDebug = Menu.GetChecked(UIPath);
            ConditionLog.RunningDebug = Menu.GetChecked(RunningPath);


            DebugLoad.LoadData = Menu.GetChecked(DataLoadPath);
            DebugLoad.LoadPackage = Menu.GetChecked(PackageLoadPath);
            DebugLoad.LoadStorage = Menu.GetChecked(StorageLoadPath);
        }


        #region Debug
        [MenuItem(MsgPath)]
        public static void MsgDebug()
        {
            Menu.SetChecked(MsgPath, !Menu.GetChecked(MsgPath));
            Debug.Log("Msg:" + Menu.GetChecked(MsgPath));
        }

        [MenuItem(ScriptPath)]
        public static void ScriptDebug()
        {
            Menu.SetChecked(ScriptPath, !Menu.GetChecked(ScriptPath));
            Debug.Log("Msg:" + Menu.GetChecked(ScriptPath));
        }



        [MenuItem(BasicPath)]
        public static void RADDebug()
        {
            Menu.SetChecked(BasicPath, !Menu.GetChecked(BasicPath));
            Debug.Log("Basic:" + Menu.GetChecked(BasicPath));
        }

        [MenuItem(ToolsPath)]
        public static void ToolsDebug()
        {
            Menu.SetChecked(ToolsPath, !Menu.GetChecked(ToolsPath));
            Debug.Log("Tools:" + Menu.GetChecked(ToolsPath));
        }
        [MenuItem(UIPath)]
        public static void UIDebug()
        {
            Menu.SetChecked(UIPath, !Menu.GetChecked(UIPath));
            Debug.Log("UI:" + Menu.GetChecked(UIPath));
        }
        [MenuItem(RunningPath)]
        public static void RunningDebug()
        {
            Menu.SetChecked(RunningPath, !Menu.GetChecked(RunningPath));
            Debug.Log("Running:" + Menu.GetChecked(RunningPath));
        }
        #endregion

        #region Load

        [MenuItem(DataLoadPath)]
        public static void LoadData()
        {
            Menu.SetChecked(DataLoadPath, true);
            Menu.SetChecked(PackageLoadPath, false);
        }

        [MenuItem(PackageLoadPath)]
        public static void LoadPackage()
        {
            Menu.SetChecked(DataLoadPath, true);
            Menu.SetChecked(PackageLoadPath, true);
        }

        [MenuItem(StorageLoadPath)]
        public static void LoadStorage()
        {
            Menu.SetChecked(StorageLoadPath, !Menu.GetChecked(StorageLoadPath));
        }

        #endregion
    }
}