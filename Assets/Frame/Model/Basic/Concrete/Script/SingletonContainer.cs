using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class SingletonContainer : ISingletonContainer
    {
        private Dictionary<string, Dictionary<string, object>> singletonDictionary = new Dictionary<string, Dictionary<string, object>>();

        public object GetSingleton(string scriptType, string scripToken, string singletonToken)
        {
            object tmp = null;
            if (singletonDictionary.ContainsKey(scriptType))
            {
                if (singletonDictionary[scriptType].ContainsKey(singletonToken))
                {
                    tmp = singletonDictionary[scriptType][singletonToken];
                }
                else
                {
                    tmp = Basic.Instence.GetInstence(scriptType, "", scripToken);
                    singletonDictionary[scriptType].Add(singletonToken, tmp);
                }
            }
            else
            {
                tmp = Basic.Instence.GetInstence(scriptType, "", scripToken);
                singletonDictionary.Add(scriptType, new Dictionary<string, object>() { { singletonToken, tmp } });
            }
            return tmp;
        }
    }
}