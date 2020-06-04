using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camStabilizer : MonoBehaviour 
{

    public PlayerMovement2 grapplingScript;

    public Quaternion rotation;
    void Awake()
    {
        rotation = transform.rotation;
    }

 
    void LateUpdate()
    {
        if (grapplingScript.isGrappling)
        {
            transform.rotation = rotation;
        }
    }
}
