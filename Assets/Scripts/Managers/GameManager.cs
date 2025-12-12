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

    private int score = 0;

    public TextMeshProUGUI visualScoreOnBeat;

    public GameObject player;
    public PlayerMovement playerMovement;

    public bool playerIsWrappingInScreen = false;


    public bool startedLevel = false;

    public float volume = 1.0f;

    public bool isOnBeat = false;
    public bool pressedOnceOnBeat = false;

    private float visualScoreTimer = 0f;
    private float visualScoreTime = 2f;

    private bool doThisOnce = true;

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
            Destroy(instance.gameObject);
        instance = this;

        DontDestroyOnLoad(instance);
    }

    private void Update()
    {
        if (doThisOnce && startedLevel && RhythmManager.Instance != null)
        {
            Debug.Log(volume);
            RhythmManager.Instance.changeVolume(volume);
            doThisOnce = false;
        }

        if (RhythmManager.Instance != null && startedLevel == true)
        {
            if (RhythmManager.Instance.timePositionMs >= RhythmManager.Instance.lengthOfSongMs)
            {
                GameManager.Instance.startedLevel = false;
                GameManager.Instance.NotifyPlayerWon();
            }

            if (!isOnBeat)
            {
                pressedOnceOnBeat = false;
            }

            setOnActiveBeat();

            //update visual score with player if timer ran out then not active, if pressed on active beat visual score is active

            visualScoreOnBeat.transform.parent.transform.position = player.transform.position;

            if (visualScoreOnBeat.gameObject.activeSelf)
            {
                visualScoreTimer += Time.deltaTime;
            }

            if (visualScoreTimer >= visualScoreTime)
            {
                visualScoreTimer -= visualScoreTime;
                visualScoreOnBeat.gameObject.SetActive(false);
            }

            if (pressedOnceOnBeat)
                visualScoreOnBeat.gameObject.SetActive(true);

        }
    }

    public void NotifyPlayerDeath()
    {
        // save final score then go to game over screen
        finalScore = score;
        SceneManager.LoadScene("GameOver");
    }

    public void NotifyPlayerWon()
    {
        // save final score then go to game over screen
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
    }

    private void setOnActiveBeat()
    {
        if (RhythmManager.Instance.activeBeat != -1)
        {
            isOnBeat = true;
        }
        else
        {
            isOnBeat = false;
        }
    }
}
