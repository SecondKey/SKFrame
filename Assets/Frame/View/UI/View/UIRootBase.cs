using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class UIRootBase : UILogicNodeBase, ITreeRoot
    {
        public override ITreeRoot Root { get { return this; } }

        public Dictionary<string, EventHandler> RootEventHandlers { get; set; }
        public void HandleRootEvent(EventArgs args)
        {
            throw new System.NotImplementedException();
        }

    }
}