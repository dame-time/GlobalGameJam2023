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
            LevelTransitionEffect.instance.StartTransition();

            yield return new WaitForSeconds(0.8f);

            GameManager.Instance.LoadNextLevel();
        }

    }
}
