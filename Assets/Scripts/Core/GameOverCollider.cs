using Core;
using UnityEngine;

public class GameOverCollider : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            gameManager.GameOver();
        }
    }
}
