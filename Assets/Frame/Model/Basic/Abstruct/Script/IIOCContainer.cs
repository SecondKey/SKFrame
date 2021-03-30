using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IIOCContainer
    {
        object CreateInstence(string scriptType, string scriptToken);
    }
}