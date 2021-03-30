using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public static class UITools
    {
        public static IEnumerable GetInsideUI(this GameObject node)
        {
            foreach (Transform t in node.transform)
            {
                if (t.GetComponent<IUILogicalNode>() != null)
                    continue;
                foreach (GameObject g in t.gameObject.GetInsideUI())
                {
                    yield return g;
                }
                yield return t.gameObject;
            }
        }
    }
}