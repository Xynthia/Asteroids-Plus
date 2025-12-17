using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // GM's singleton for easy access throughout the whole project
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public static int finalScore = 0;

    
    public TextMeshProUGUI scoreTextObject = null;
    public TextMeshProUGUI visualScoreOnBeat;
    
    private int score = 0;
    private float visualScoreTimer = 0f;
    private float visualScoreTime = 2f;
    private bool visualScoreOn = false;


    public GameObject player;
    public PlayerMovement playerMovement;
    public bool playerIsWrappingInScreen = false;


    public bool startedLevel = false;
    public bool skipToSongSelect = false;

    public bool isOnBeat = false;
    public bool pressedOnceOnBeat = false;


    private enum visualScoreName
    {
        Early = 1,
        Good = 2,
        Perfect = 3,
        Almost = 4,
        Late = 5
    }



    private void Awake()
    {
        // setup singleton
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    private void Update()
    {
        if (RhythmManager.Instance != null && startedLevel == true)
        {
            if (RhythmManager.Instance.sampleTimeS >= RhythmManager.Instance.lengthOfSongS)
            {
                GameManager.Instance.startedLevel = false;
                GameManager.Instance.NotifyPlayerWon();
            }

            if (!isOnBeat)
            {
                pressedOnceOnBeat = false;
            }


            // visual score follows player goes off after visualscoretime.
            if (visualScoreOnBeat != null)
                visualScoreOnBeat.transform.parent.transform.position = player.transform.position;

            if (visualScoreOn)
            {
                visualScoreTimer += Time.deltaTime;
            }

            if (visualScoreTimer >= visualScoreTime)
            {
                visualScoreTimer -= visualScoreTime;
                scoreTextObject.gameObject.SetActive(false);
            }
        }

        Debug.Log(skipToSongSelect);
    }

    public void NotifyPlayerDeath()
    {
        visualScoreOnBeat = null;
        HighScoreManager.Instance.setHighScore(score);
        finalScore = score;
        SceneManager.LoadScene("GameOver");
    }

    public void NotifyPlayerWon()
    {
        visualScoreOnBeat = null;
        HighScoreManager.Instance.setHighScore(score);
        finalScore = score;
        SceneManager.LoadScene("GameWon");
    }

    public void AddScore(int amount)
    {
        // increase score and update UI
        score += amount;

        if (scoreTextObject != null)
            scoreTextObject.text = "Score: " + score.ToString();
    }

    public void playerPressedButton()
    {
        pressedOnceOnBeat = true;

        playerMovement.move();

        CheckScore();
    }

    // add score based on precision
    public void CheckScore()
    {
        float percentageAfterBeat = RhythmManager.Instance.checkPercentagMargin();

        if (percentageAfterBeat < 10){
            setVisualScore(visualScoreName.Early);
            AddScore(1);
        }
        else if (percentageAfterBeat < 25){
            setVisualScore(visualScoreName.Early);
            AddScore(2);
        }
        else if (percentageAfterBeat < 45){
            setVisualScore(visualScoreName.Good);
            AddScore(3);
        }
        else if (percentageAfterBeat < 55){
            setVisualScore(visualScoreName.Perfect);
            AddScore(5);
        }
        else if (percentageAfterBeat < 75){
            setVisualScore(visualScoreName.Almost);
            AddScore(3);
        }
        else if (percentageAfterBeat < 90){
            setVisualScore(visualScoreName.Late);
            AddScore(2);
        }
        else if (percentageAfterBeat < 100){
            setVisualScore(visualScoreName.Late);
            AddScore(1);
        }
    }

    private void setVisualScore(visualScoreName visualScore)
    {
        switch (visualScore)
        {
            case visualScoreName.Early:
                visualScoreOnBeat.text = "Early";
                visualScoreOnBeat.color = Color.orange;
                break;
            case visualScoreName.Good:
                visualScoreOnBeat.text = "Good";
                visualScoreOnBeat.color = Color.green;
                break;
            case visualScoreName.Perfect:
                visualScoreOnBeat.text = "Perfect";
                visualScoreOnBeat.color = Color.gold;
                break;
            case visualScoreName.Almost:
                visualScoreOnBeat.text = "Almost";
                visualScoreOnBeat.color = Color.yellow;
                break;
            case visualScoreName.Late:
                visualScoreOnBeat.text = "Late";
                visualScoreOnBeat.color = Color.red;
                break;
        }

        visualScoreOnBeat.gameObject.SetActive(true);
    }
}
