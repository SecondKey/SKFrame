using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface ITreeRoot
    {
        Dictionary<string, EventHandler> RootEventHandlers { get; set; }
        void HandleRootEvent(EventArgs args);
    }
}