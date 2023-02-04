using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns
{
    public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance is null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        instance = obj.AddComponent<T>();
                    }

                    DontDestroyOnLoad(instance.gameObject);
                }

                return instance;
            }
        }
    }
}
