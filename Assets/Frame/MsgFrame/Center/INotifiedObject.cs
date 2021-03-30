using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface INotifiedObject
    {
        string MsgStstem { get; }
        Dictionary<string, IMsgHandler> MsgHandlers { get; set; }

        void ProcessEvent(MsgBase msg);
        void OnDestroy();
    }
}