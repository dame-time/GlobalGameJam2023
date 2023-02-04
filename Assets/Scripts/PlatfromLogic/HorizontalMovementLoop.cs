using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovementLoop : MonoBehaviour
{
    public float speed = 2.0f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        var nextPosition = startPosition + (Mathf.Sin(Time.time * speed) * Vector3.right);
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
    }
}
