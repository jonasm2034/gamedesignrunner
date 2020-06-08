using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(menuName = "LevelChunkData")]
public class LevelChunkData : ScriptableObject
{
    public enum Direction
    {
        Var1, Var2, Var3, Var4

    }

    public Vector2 chunkSize = new Vector2(10f, 10f);

    public GameObject[] levelChunks;

    
    public Direction beginVar;

    
    public Direction exitVar;
    
}
