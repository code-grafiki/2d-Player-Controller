using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPersist : MonoBehaviour
{

    void Awake()
    {
        int numScreenPersist = FindObjectsOfType<ScreenPersist>().Length;
        if (numScreenPersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public void ResetScreenPersist()
    {
        Destroy(gameObject);
    }
}
