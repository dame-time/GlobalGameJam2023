using System.IO;
using UnityEngine;

public class DestroyFile : MonoBehaviour
{
    public string path;

    private bool exists = false;

    private void Start()
    {
        
    }

    public void SetPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }

        exists = File.Exists(path);
        this.path = path;
    }

    void FixedUpdate()
    {
        if(!exists)
        {
            return;
        }

        if(!File.Exists(path))
        {
            Destroy(gameObject);
        }
    }
}
