using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public sealed class NextLevel : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.layer == 3)
                GameManager.Instance.LoadNextLevel();
        }
    }
}
