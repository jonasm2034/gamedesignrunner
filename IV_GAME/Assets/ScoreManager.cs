using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    public int score, highscore;

    // Start is called before the first frame update

    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore()
    {
        //score = (score + 1) / 2;
        score++;

        UpdateHighScore();
    }

    public void UpdateHighScore()
    {
        highscore = score;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
