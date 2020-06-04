using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefabGrid : MonoBehaviour
{

    public GameObject[] itemsToPickFrom;
    public int gridZ;
    public float gridSpacingOffset = 1f;
    public Vector3 gridOrigin = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        SpawnGrid();
    }

    void SpawnGrid()
    {
        for (int z = 0; z < gridZ; z++)
        {
            Vector3 spawnPosition = new Vector3(0, 0, z * gridSpacingOffset) + gridOrigin;
            //PickAndSpawn(spawnPosition, Quaternion.identity);
        }
    }
    
}
