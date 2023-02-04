using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UserInterface.Buttons
{
    public class SettingsMenuButtons : MonoBehaviour
    {
        public void OnVolumeChange() => Debug.Log(GetComponentInChildren<Scrollbar>().value);
        public void BackToMainMenu() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
