using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        public Scene LoadNewScene()
        {
            var scene = SceneManager.CreateScene("New Scene");
            return scene;
        }
    }
}
