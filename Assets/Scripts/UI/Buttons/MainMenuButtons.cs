using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UserInterface.Buttons
{
    public class MainMenuButtons : MonoBehaviour
    {
        public void StartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        public void Settings() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

        public void QuitGame() => Application.Quit();
    }
}
