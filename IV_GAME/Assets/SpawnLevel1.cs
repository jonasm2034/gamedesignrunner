using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevel1 : MonoBehaviour
{

    private int x = 4;
    private int y = 1;

    public GameObject startBlock; //start Block
    public GameObject[] blockPrefabs; //Prefabs für alle BlockVarianten

    private GameObject currentBlock;
    private GameObject middleBlock; // mittel Block
    private GameObject nextBlock; //nächster Block
    


        void Start() //StartBlock
        {

            currentBlock = Instantiate(startBlock, transform.position*20, transform.rotation, transform); // spawned Startblock
            middleBlock = Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Length)], currentBlock.transform.GetChild(0).transform.position, transform.rotation, transform); // spawned Zweiten Block, zufällige Auswahl
            nextBlock = Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Length)], middleBlock.transform.GetChild(0).transform.position, transform.rotation, transform); // spawned Zweiten Block, zufällige Auswahl

            middleBlock.GetComponent<TriggerNextSpawn>().spawner = this; // weist dem Trigger Script dieses Script zu
            nextBlock.GetComponent<TriggerNextSpawn>().spawner = this; // weist dem Trigger Script dieses Script zu


        }

        
    
        public void SpawnNextBlock()
        {
            Destroy(currentBlock, 5); // zerstört den alten Block
            currentBlock = middleBlock;
            middleBlock = nextBlock;
            

            
            nextBlock = Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Length)], middleBlock.transform.GetChild(0).transform.position, transform.rotation, transform); // spawned neuen Block
            nextBlock.GetComponent<TriggerNextSpawn>().spawner = this; // weißt dem Trigger Script vom neuen Block dieses Script zu
                                        
        }

        private void Update()
        {
        
                    if (Input.GetKeyDown(KeyCode.T))
                    {
                        SpawnNextBlock();
                    }

            
        }
        

    



}
