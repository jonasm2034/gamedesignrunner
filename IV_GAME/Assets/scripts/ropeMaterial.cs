using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ropeMaterial : MonoBehaviour
{
    Renderer rend; 
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void LateUpdate()
    {
        rend.material.mainTextureScale = new Vector2(1, 120*transform.localScale.y);
    }
}
