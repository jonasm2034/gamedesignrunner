using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    public int score, highscore;
    public Text scoreText, highscoreText, gameOverSscoreText;

    // Start is called before the first frame update


    public void Awake()
    {
        instance = this;

        highscore = PlayerPrefs.GetInt("HighScore");
        gameOverSscoreText.text = score.ToString();
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

        scoreText.text = score.ToString();
        gameOverSscoreText.text = score.ToString();
    }

    public void UpdateHighScore()
    {
        if (score > highscore)
        {
            highscore = score;

            highscoreText.text = highscore.ToString();

            PlayerPrefs.SetInt("highscore", highscore);
        }
        
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
        gameOverSscoreText.text = score.ToString();
    }
}
