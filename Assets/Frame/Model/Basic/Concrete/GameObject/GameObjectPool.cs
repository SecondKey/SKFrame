using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class GameObjectPool : IGameObjectPool
    {
        Dictionary<string, List<object>> ActiveObject = new Dictionary<string, List<object>>();
        Dictionary<string, List<object>> IdleObject = new Dictionary<string, List<object>>();

        public object GetIdleObject(EventArgs args)
        {
            ObjectArgs tmpArgs = args as ObjectArgs;
            object tmpObject;
            if (IdleObject.ContainsKey(tmpArgs.Group))
            {
                tmpObject = IdleObject[tmpArgs.Group][0];
            }
            else
            {
                tmpObject = Basic.Instence.GetInstence(tmpArgs.ScriptType, tmpArgs.Performer as string, tmpArgs.ScriptToken);
            }
            if (ActiveObject.ContainsKey(tmpArgs.Group))
            {
                ActiveObject[tmpArgs.Group].Add(tmpObject);
            }
            else
            {
                ActiveObject.Add(tmpArgs.Group, new List<object>() { tmpObject });
            }
            return tmpObject;
        }
    }
}