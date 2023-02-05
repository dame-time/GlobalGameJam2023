using Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FolderPathUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        text.text = GameManager.Instance.GetUpFolder();
    }
}
