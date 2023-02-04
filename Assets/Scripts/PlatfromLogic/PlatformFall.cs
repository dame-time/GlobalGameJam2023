using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class PlatformFall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.IsTouchingLayers(3))
            StartCoroutine(WaitAndFall());
            
    }

    IEnumerator WaitAndFall()
    {
        yield return new WaitForSeconds(0.3f);

        this.GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
