using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    [Initialization(AppConst.Init_HandlerEventBinding)]
    [Initialization(AppConst.Init_GetterEventBinding)]
    public class GameObjectManager : BasicManagerBase
    {
        [Inject]
        IAssembler assembler { get; set; }
        [Inject]
        IGameObjectPool gameObjectPool { get; set; }

        #region Handler
        [HandlerEventBinding("Load")]
        void Load(EventArgs args)
        {
            EventArgs<IState<string>> tmpArgs = args as EventArgs<IState<string>>;
            ConditionLog.BasicLog($"Start Load {Identify} GameObject");
            tmpArgs.Parameter.ReductionState();
        }
        #endregion

        #region Getter
        public GameObject GetNewGameObject(string objectName)
        {
            return null;
        }

        public GameObject GetGameObject(string objectName)
        {
            return null;
        }
        #endregion

    }
}