using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    [Initialization(AppConst.Init_HandlerEventBinding)]
    [Initialization(AppConst.Init_GetterEventBinding)]
    public class ScriptManager : BasicManagerBase
    {
        [Inject]
        IIOCContainer iocContainer { get; set; }
        [Inject]
        ISingletonContainer singletonContainer { get; set; }
        [Inject]
        IObjectPool objectPool { get; set; }


        #region Handler
        [HandlerEventBinding("Load")]
        void Load(EventArgs args)
        {
            EventArgs<IState<string>> tmpArgs = args as EventArgs<IState<string>>;
            ConditionLog.BasicLog($"Start Load {Identify} Script");
            tmpArgs.Parameter.ReductionState();
        }
        #endregion 

        #region Getter
        #region NewInstence
        [GetterEventBinding("GetInstence")]
        object GetInstence(EventArgs args)
        {
            InstenceArgs tmpArgs = args as InstenceArgs;
            return iocContainer.CreateInstence(tmpArgs.ScriptType, tmpArgs.ScriptToken);
        }
        #endregion

        #region Singleton
        [GetterEventBinding("GetSingleton")]
        public object GetSingleton(EventArgs args)
        {
            SingletonArgs tmpArgs = args as SingletonArgs;
            return singletonContainer.GetSingleton(tmpArgs.ScriptType, tmpArgs.ScriptToken, tmpArgs.SingletonToken);
        }
        #endregion
        #endregion

    }
}