using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAndKillPlayer : MonoBehaviour, IResettable
{
    public int force = 5;
    public bool isCollided = false;
    public Vector3 startPos;

    public void Reset()
    {
        isCollided = false;
        transform.position = startPos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isCollided = true;
            startPos = transform.position;
        }
    }

    private void Update()
    {
        if(isCollided)
        {
            transform.parent.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.left, 2 * Time.deltaTime);
        }
    }
}
