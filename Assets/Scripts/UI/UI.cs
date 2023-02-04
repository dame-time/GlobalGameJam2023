using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Core.UserInterface
{
    public sealed class UI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI pathText;

        private void Awake() {
            if (FindObjectsOfType <UI>().Length > 1)
                Destroy(gameObject);
            else
                DontDestroyOnLoad(gameObject);
        }

        public void SetPathText(string path) => pathText.text = path;
    }
}
