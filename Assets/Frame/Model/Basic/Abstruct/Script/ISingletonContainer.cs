using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface ISingletonContainer
    {
        object GetSingleton(string scriptType, string scripToken, string singletonToken);
    }
}