using Core;
using UnityEngine;

public class GameOverCollider : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            GameManager.Instance.GameOver();
        }
    }
}
