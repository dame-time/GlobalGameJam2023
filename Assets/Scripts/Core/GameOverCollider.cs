using Core;
using UnityEngine;

public class GameOverCollider : MonoBehaviour
{
    [SerializeField] private AudioClip[] dies;
    [SerializeField] private AudioSource audioSource;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            audioSource.PlayOneShot(dies[Random.Range(0, dies.Length)]);
            GameManager.Instance.GameOver();
        }
    }
}
