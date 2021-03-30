using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class RunTimeTools : MonoBehaviour
    {
        public static RunTimeTools instence;

        private void Awake()
        {
            instence = this;
        }

        public new void StartCoroutine(IEnumerator routine)
        {
            base.StartCoroutine(routine);
        }
    }
}