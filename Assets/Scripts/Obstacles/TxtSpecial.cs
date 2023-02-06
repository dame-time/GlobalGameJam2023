using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TxtSpecial : MonoBehaviour
{
    [SerializeField] Transform docOne;
    [SerializeField] Transform docTwo;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Vector3 cachedPos = docOne.position;
            docOne.position = docTwo.position;
            cachedPos.y -= 0.3f;
            docTwo.position = cachedPos;
            GetComponent<AudioSource>().Play();
        }
    }
}
