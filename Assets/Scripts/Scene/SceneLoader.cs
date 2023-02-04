using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.SceneManagement
{
    public class SceneLoader
    {
        public static void LoadNewScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
