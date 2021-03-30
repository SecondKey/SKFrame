using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class TestInitScene : InitializedGameObject
    {
        public override void Initialization()
        {
        }

        public void LoadScene()
        {
            SceneManager.LoadScene(1);
        }
    }
}