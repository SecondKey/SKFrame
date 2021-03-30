using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public static class AppConst
    {
        //请不要修改Auto的数据
        #region Auto
        #region IOC/Initialize

        public const string Injection_Static = "Static";
        /// <summary>
        /// 使用事件中的值
        /// </summary>
        public const string Injection_Additional = "Additional";
        /// <summary>
        /// 使用DataCenter中的值
        /// </summary>
        public const string Injection_InternalData = "Internal";
        /// <summary>
        /// 使用外部的值(调用DataCenter.GetData)
        /// </summary>
        public const string Injection_ExternalData = "External";
        /// <summary>
        /// 使用外部的动态数据
        /// </summary>
        public const string Injection_DynamicData = "Dynamic";
        /// <summary>
        /// 创建一个新的对象
        /// </summary>
        public const string Injection_NewInstence = "NewInstence";
        /// <summary>
        /// 创建一个新的游戏物体
        /// </summary>
        public const string Injection_NewGameObject = "NewGameObject";
        /// <summary>
        /// 获取一个对象，该对象可以是已经在对象池中的对象
        /// </summary>
        public const string Injection_GameObject = "GameObject";

        public const string Init_Inject = "Inject";
        public const string Init_HandlerEventBinding = "HandlerEventBinding";
        public const string Init_GetterEventBinding = "GetterEventBinding";
        public const string Init_UIBinding = "UIBinding";

        public const string InitMode_Before = "InitMode_Before";
        public const string InitMode_After = "InitMode_After";

        #endregion

        #region Asset
        public enum AssetLevel
        {
            Internal = -1,
            Root = 0,
            General = 10,
            Additional = 30,
            Running = 40,
            RunningAdditional = 50
        }

        public const string RootLevel = "Root";
        public const string GeneralLevel = "General";
        public const string AdditionalLevel = "Additional";
        public const string RunningLevel = "Running";
        public const string RunningAdditionalLevel = "RunningAdditional";

        public static int GetAssetLevelNum(this string assetName)
        {
            return (int)(AssetLevel)Enum.Parse(typeof(AssetLevel), assetName);
        }
        #endregion

        #region Msg
        public const string MsgHandler_Common = "Common";
        public const string MsgHandler_Wait = "Wait";
        #endregion
        #endregion

        #region Modifiable  
        #region Path
        public static string DataPath { get { return Application.dataPath + "/"; } }

        public static Dictionary<string, string> AssetPath = new Dictionary<string, string>();

        //public const string RootPath = "GameAssets/Root";
        //public const string GeneralPath = "GameAssets/General";
        //public const string UpdatePath = "Update";
        //public const string DynamicDataPath = "Dynamic";
        //public const string SaveDataPath = "Save";
        //public const string DLCPath = "DLC";
        //public const string MODPath = "MOD";

        #endregion

        #region IOC

        #endregion

        #region Asset
        public const string RootData = "RootData";
        public const string RootBundle = "RootnBundle";
        #endregion

        #region MsgSystem
        public const string MsgSystem_RootModel = "RootModel";
        #endregion

        #endregion
    }
}