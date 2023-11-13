using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundClips : MonoBehaviour
{
    public static BackgroundClips Instance { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        // Prevent object from being destroyed
         if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}


  