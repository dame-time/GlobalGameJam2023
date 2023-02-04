using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAndKillPlayer : MonoBehaviour
{
    public int force = 5;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            this.GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            this.GetComponentInParent<Rigidbody2D>().AddForce((GameObject.FindAnyObjectByType<GameManager>().Player.transform.position - this.transform.position) * force);
        }
    }
}
