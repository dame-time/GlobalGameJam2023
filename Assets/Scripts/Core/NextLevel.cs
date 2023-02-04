using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public sealed class NextLevel : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.gameObject.layer == 3)
            {
                StartCoroutine(TransitionToNextLevel());
            }
        }

        private IEnumerator TransitionToNextLevel()
        {
            yield return LevelTransitionEffect.instance.StartTransition(GameManager.Instance.LoadNextLevel, true);
        }

    }
}
