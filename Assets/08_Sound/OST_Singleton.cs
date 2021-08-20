using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OST_Singleton : MonoBehaviour
{
    private static OST_Singleton _instance;

    public static OST_Singleton Instance { get { return _instance; } }


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
