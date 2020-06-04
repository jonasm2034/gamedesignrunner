using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevel : MonoBehaviour
{
    public GameObject startBlock; //start Block
    public GameObject[] blockPrefabs; //Prefabs für alle BlockVarianten

    private GameObject currentBlock;
    
    private GameObject nextBlock; //nächster Block
    


        void Start() //StartBlock
        {

            currentBlock = Instantiate(startBlock, transform.position, transform.rotation, transform); // spawned Startblock
            nextBlock = Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Length)], currentBlock.transform.GetChild(0).transform.position, transform.rotation, transform); // spawned Zweiten Block, zufällige Auswahl
        
            nextBlock.GetComponent<TriggerNextSpawn>().spawner = this; // weist dem Trigger Script dieses Script zu

        }

        public void SpawnNextBlock()
        {
            currentBlock = nextBlock;
            //Destroy(currentBlock, 5); // zerstört den alten Block
                 
            nextBlock = Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Length)], currentBlock.transform.GetChild(0).transform.position, transform.rotation, transform); // spawned neuen Block

            nextBlock.GetComponent<TriggerNextSpawn>().spawner = this; // weißt dem Trigger Script vom neuen Block dieses Script zu

            

        }

        

    



}
