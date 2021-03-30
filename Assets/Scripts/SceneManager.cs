using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class SceneManager : MonoBehaviour
    {
        private void Awake()
        {

        }

        public void InitObject()
        {

        }

        public static void LoadScene(int sceneNum)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNum);
        }
    }
}