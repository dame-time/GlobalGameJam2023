using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UserInterface.Buttons
{
    public class PauseMenuButtons : MonoBehaviour
    {
        public void OnVolumeChange() => Debug.Log(GetComponentInChildren<Scrollbar>().value);
        public void BackToMainMenu() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        public void QuitGame() => Application.Quit();
    }
}
