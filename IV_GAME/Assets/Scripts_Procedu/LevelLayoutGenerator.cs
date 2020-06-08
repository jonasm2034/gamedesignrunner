using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLayoutGenerator : MonoBehaviour
{
    public LevelChunkData[] levelChunkData;
    public LevelChunkData firstChunk;


    private LevelChunkData previousChunk;


    public Vector3 spawnOrigin;

    
    private Vector3 spawnPosition;
    public int chunksToSpawn = 5;


    //um zu wissen ob man das Level beendet hat und in ein neues geht
    
    void OnEnable()
    {

        TriggerExit.OnChunkExited += PickAndSpawnChunk;

    }

    private void OnDisable()
    {
        TriggerExit.OnChunkExited -= PickAndSpawnChunk;
    }
    
    


    //nur zum Debuggen



    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            PickAndSpawnChunk();
        }
    }




    // am anfang werden die Chunks/Level gespawnt
    void Start()
    {
        previousChunk = firstChunk;

        for (int i = 0; i < chunksToSpawn; i++)
        {
            PickAndSpawnChunk();

        }
    }



    LevelChunkData PickNextChunk()
    {
        List<LevelChunkData> allowedChunkList = new List<LevelChunkData>();
        LevelChunkData nextChunk = null; //wartet um gefüllt zu werden

        
        LevelChunkData.Direction nextRequiredDirection = LevelChunkData.Direction.Var1; //default Direction ist Norden

        switch (previousChunk.exitVar) //wird aber überschrieben
        {


            case LevelChunkData.Direction.Var1: //wenn der Ausgang des vorherigen Chunks norden war
                nextRequiredDirection = LevelChunkData.Direction.Var1; //also brauchen wir einen Eingang vom nächsten Chunk vom Süden aus
                spawnPosition = spawnPosition + new Vector3(0f, 0, previousChunk.chunkSize.y*80); //offset

                break;



           /* case LevelChunkData.Direction.East:
                nextRequiredDirection = LevelChunkData.Direction.West;
                spawnPosition = spawnPosition + new Vector3(previousChunk.chunkSize.x, 0, 0);

                break;
*/


            /*case LevelChunkData.Direction.South:
                nextRequiredDirection = LevelChunkData.Direction.North;
                spawnPosition = spawnPosition + new Vector3(0f, 0, -previousChunk.chunkSize.y);

                break;



            case LevelChunkData.Direction.West:
                nextRequiredDirection = LevelChunkData.Direction.East;
                spawnPosition = spawnPosition + new Vector3(-previousChunk.chunkSize.x, 0, 0);
*/
                break;
            default:
                break;

        }


        for (int i = 0; i < levelChunkData.Length; i++)
        {
            if (levelChunkData[i].beginVar == nextRequiredDirection) // alle die den gewünschten Eingang haben wie ob beschrieben werden einer Liste hinzugefügt
            {
                allowedChunkList.Add(levelChunkData[i]);
            }
        }

        nextChunk = allowedChunkList[Random.Range(0, allowedChunkList.Count)]; //zufällige Auswahl der erlaubten Chunks

        return nextChunk;
    }





    void PickAndSpawnChunk()
    {
        LevelChunkData chunkToSpawn = PickNextChunk(); //wähle jetzt einen Chunk aus den ich konnekten kann

        GameObject objectFromChunk = chunkToSpawn.levelChunks[Random.Range(0, chunkToSpawn.levelChunks.Length)];
        previousChunk = chunkToSpawn;
        Instantiate(objectFromChunk, spawnPosition + spawnOrigin, Quaternion.identity);
    
    }

    public void UpdateSpawnOrigin(Vector3 originDelta)
    {
        spawnOrigin = spawnOrigin + originDelta;
    }
}
