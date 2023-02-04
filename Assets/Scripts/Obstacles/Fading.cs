using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fading : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
            StartCoroutine(WaitAndFade());
    }

    IEnumerator WaitAndFade()
    {
        yield return new WaitForSeconds(0.3f);

        this.enabled = false;
        this.transform.parent.gameObject.SetActive(false);
    }
}
