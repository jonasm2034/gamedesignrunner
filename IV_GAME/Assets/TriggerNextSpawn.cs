using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNextSpawn : MonoBehaviour
{
    public SpawnLevel spawner; // andere Klasse wird reingeladen

    int x = 3;

    void Start()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            
            spawner.SpawnNextBlock();

        }
    }

    
}
