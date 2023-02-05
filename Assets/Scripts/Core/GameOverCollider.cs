using Core;
using UnityEngine;

public class GameOverCollider : MonoBehaviour
{
    [SerializeField] private AudioClip[] dies;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject resettableGO;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            if(resettableGO)
            {
                IResettable resettable = resettableGO.GetComponent<IResettable>();
                if(resettable != null)
                {
                    resettable.Reset();
                }
            }

            audioSource.PlayOneShot(dies[Random.Range(0, dies.Length)]);
            GameManager.Instance.GameOver();
        }
    }
}
