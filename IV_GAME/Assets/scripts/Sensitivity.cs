using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Sensitivity : MonoBehaviour
{

    public static float sensitivity = 100f;

    //private void Update()
    //{
    //    Debug.Log(sensitivity);
    //}

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void AdjustSensitivity (float newSensitivity)
    {
        sensitivity = newSensitivity;
    }
 
}
