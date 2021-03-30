using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using UnityEngine;

namespace GSFramework
{
    public class Basic
    {

        public ListState<string> LoadLevelState { get; private set; } = new ListState<string>("");
        IDataNode assetManager;
        IDataNode scriptManager;
        IDataNode gameObjectManager;

        public CommonState<bool> UpdateState { get; private set; } = new CommonState<bool>(false);

        IDynamicDataContainer dynamicData;

        #region 单例
        private static Basic instence;
        private Basic()
        {
            LoadAppConfig();

            LoadLevelState.AddState((s) => { assetManager.HandleEvent(new EventArgs<IState<string>>("Load", LoadLevelState, s)); });
            LoadLevelState.AddState((s) => { scriptManager.HandleEvent(new EventArgs<IState<string>>("Load", LoadLevelState, s)); });
            LoadLevelState.AddState((s) => { gameObjectManager.HandleEvent(new EventArgs<IState<string>>("Load", LoadLevelState, s)); });
        }

        public static Basic Instence
        {
            get
            {
                if (instence == null)
                {
                    instence = new Basic();
                }
                return instence;
            }
        }
        #endregion

        #region AppConfig
        private Dictionary<string, Dictionary<string, Type>> basicMapping = new Dictionary<string, Dictionary<string, Type>>();

        public void LoadAppConfig()
        {
            XDocument config = XDocument.Load(AppConst.DataPath + "AppConfig.xml");

            foreach (XElement path in config.Root.Element("Path").Elements())
            {
                AppConst.AssetPath.Add(path.Name.ToString(), AppConst.DataPath + path.Value + "/");
            }

            foreach (XElement scriptType in config.Root.Element("IOCMappings").Elements())
            {
                basicMapping.Add(scriptType.Name.ToString(), new Dictionary<string, Type>());
                if (!string.IsNullOrEmpty(scriptType.Value))
                {
                    basicMapping[scriptType.Name.ToString()].Add("", Type.GetType(scriptType.Value));
                }
                foreach (XElement scriptToken in scriptType.Elements())
                {
                    basicMapping[scriptType.Name.ToString()].Add(scriptToken.Name.ToString(), Type.GetType(scriptToken.Value));
                }
            }
        }

        object CreateInstence(string scriptType, string scriptToken)
        {
            if (!basicMapping.ContainsKey(scriptType) || !basicMapping[scriptType].ContainsKey(scriptToken))
            {
                return null;
            }

            return basicMapping[scriptType][scriptToken].CreateInstence();
        }
        #endregion

        #region Update
        public void StartUpdate(Action<bool> over)
        {
            IUpdateManager updateManager = GetInstence<IUpdateManager>();
            UpdateState.ChangeState(true, null, over);
            updateManager.Update(UpdateState);
        }
        #endregion

        #region Load
        public void LoadLevel(string level, Action<string> over)
        {
            IDataNode tmpAssetManager = GetInstence<IDataNode>("", "AssetManager", new Dictionary<string, object>() { { "Identify", level } });
            IDataNode tmpScriptManager = GetInstence<IDataNode>("", "ScriptManager", new Dictionary<string, object>() { { "Identify", level } });
            IDataNode tmpGameObjectManager = GetInstence<IDataNode>("", "GameObjectManager", new Dictionary<string, object>() { { "Identify", level } });

            if (level == AppConst.RootLevel)
            {
                assetManager = tmpAssetManager;
                scriptManager = tmpScriptManager;
                gameObjectManager = tmpGameObjectManager;
            }
            else
            {
                assetManager.AddNode(tmpAssetManager);
                scriptManager.AddNode(tmpScriptManager);
                gameObjectManager.AddNode(tmpGameObjectManager);
            }

            LoadLevelState.TmpReductionStateEvent = over;
            LoadLevelState.ChangeState(level);
        }
        #endregion

        #region Asset
        #region Data
        public object GetData(string getMode, params string[] parameters)
        {
            return GetData(new GetDataEventArgs("GetData", getMode, parameters));
        }

        public object GetData(string getMode, string performer, params string[] parameters)
        {
            return GetData(new GetDataEventArgs("GetData", getMode, parameters, performer));
        }

        public object GetData(GetDataEventArgs args)
        {
            return assetManager.GetData(args);
        }
        #endregion

        #region Bundle
        public object GetBundleResource(string getDataToken, params string[] parameters)
        {
            return GetBundleResource(new EventArgs<string[]>(getDataToken, parameters));
        }

        public object GetBundleResource(string getDataToken, string performer, params string[] parameters)
        {
            return GetBundleResource(new EventArgs<string[]>(getDataToken, parameters, performer));
        }

        public object GetBundleResource(EventArgs<string[]> args)
        {
            return null;
            //return assetManager.GetBundleResource(args);
        }
        #endregion

        #region Resource
        public object GetResource(string getDataToken, string resourceName)
        {
            return GetResource(new EventArgs<string>(getDataToken, resourceName));
        }

        public object GetResource(string getDataToken, string performer, string resourceName)
        {
            return GetResource(new EventArgs<string>(getDataToken, resourceName, performer));
        }

        public object GetResource(EventArgs<string> args)
        {
            return null;
            //return assetManager.GetResource(args);
        }
        #endregion

        #region DynamicData
        public object GetDynamicData(string getDataToken, params string[] parameters)
        {
            return GetDynamicData(new EventArgs<string[]>(getDataToken, parameters));
        }

        public object GetDynamicData(string getDataToken, string performer, params string[] parameters)
        {
            return GetDynamicData(new EventArgs<string[]>(getDataToken, parameters, performer));
        }

        public object GetDynamicData(EventArgs<string[]> args)
        {
            return null;
            //return assetManager.GetDynamicData(args);
        }
        #endregion 
        #endregion

        #region Script
        public RecursiveScopeState<Dictionary<string, object>> CreateInstenceState { get; set; } = new RecursiveScopeState<Dictionary<string, object>>(new Dictionary<string, object>());

        #region Instence
        public T GetInstence<T>(string performer = "", string scriptToken = "", Dictionary<string, object> parameters = null)
        {
            object tmp = GetInstence(typeof(T), performer, scriptToken, parameters);
            if (tmp == null)
            {
                return default;
            }
            else
            {
                return (T)tmp;
            }
        }

        public object GetInstence(Type scriptType, string performer = "", string scriptToken = "", Dictionary<string, object> parameters = null)
        {
            return GetInstence(scriptType.FullName, performer, scriptToken, parameters);
        }

        public object GetInstence(string scriptType, string performer, string scriptToken, Dictionary<string, object> parameters = null)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }
            object tmpObject = null;
            using (CreateInstenceState.SetScope(parameters))
            {
                if (performer == "")
                {
                    tmpObject = CreateInstence(scriptType, scriptToken);
                }
                if (tmpObject == null)
                {
                    tmpObject = scriptManager.GetData(new InstenceArgs(scriptType, performer, scriptToken));
                }
                if (tmpObject == null)
                {
                    tmpObject = scriptType.CreateInstence();
                }
            }
            return tmpObject;
        }
        #endregion

        #region Singleton
        public T GetSingleton<T>(string performer = AppConst.RootLevel, string scriptToken = "", string singletonToken = "")
        {
            return (T)GetSingleton(typeof(T).FullName, performer, scriptToken, singletonToken);
        }

        public object GetSingleton(Type scriptType, string performer = AppConst.RootLevel, string scriptToken = "", string singletonToken = "")
        {
            return GetSingleton(scriptType.FullName, performer, scriptToken, singletonToken);
        }

        public object GetSingleton(string scriptType, string performer = AppConst.RootLevel, string scriptToken = "", string singletonToken = "")
        {
            return scriptManager.GetData(new SingletonArgs(scriptType, performer, scriptToken, singletonToken));
        }
        #endregion

        #region ObjectPool

        #endregion
        #endregion

        #region GameObject
        #region GameObject

        #endregion

        #region GameObjectPool
        public GameObject GetNewGameObject(string name)
        {
            return null;
            //return gameObjectManager.GetNewGameObject(name);
        }

        public GameObject GetGameObject(string name)
        {
            return null;
            //return gameObjectManager.GetGameObject(name);
        }
        #endregion
        #endregion


        #region Tools
        public static bool CheckConditions(string conditions)
        {
            return true;
        }
        #endregion
    }
}