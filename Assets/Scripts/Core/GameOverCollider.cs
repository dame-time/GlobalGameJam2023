using Core;
using UnityEngine;

public class GameOverCollider : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioClip[] dies;
    [SerializeField] private AudioSource audioSource;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            gameManager.GameOver();
            audioSource.PlayOneShot(dies[Random.Range(0, dies.Length)]);
        }
    }
}
