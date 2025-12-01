using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // GM's singleton for easy access throughout the whole project
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public static int finalScore = 0;

    [SerializeField]
    private TextMeshProUGUI scoreTextObject = null; 

    private int score = 0;

    [SerializeField]
    private GameObject player = null;
    public GameObject Player { get { return player; } }

    [SerializeField]
    private AudioSource bgAudioSource;

    public bool startedLevel = false;
    private float songTimer = 0f;

    public bool isOnBeat;
    public float safeTime = 0f;

    private float safeTimer = 0f;
    private bool startTimer = false;

    private void Awake()
    {
        // setup singleton
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;

    }

    private void Update()
    {
        // the time from start of beat to the safe margin of being on beat will set is on beat.
        if (isOnBeat == true)
        {
            safeTimer += Time.deltaTime;

            if (safeTimer > safeTime)
            {
                safeTimer = safeTimer - safeTime;
                isOnBeat = false;
            }
        }

        // will start timer at start of level and at end will show the end screen.
        if (startedLevel == true)
            songTimer += Time.deltaTime;
        
        if (songTimer >= bgAudioSource.clip.length)
        {
            startedLevel = false;
            NotifyPlayerDeath();
        }
            
    }


    public void NotifyPlayerDeath()
    {
        // save final score then go to game over screen
        finalScore = score;
        SceneManager.LoadScene("GameOver");
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
        float percentageAfterBeat;

        percentageAfterBeat = (100 / safeTime) * safeTimer;

        if (percentageAfterBeat < 10)
            AddScore(5);
        else if (percentageAfterBeat < 50) 
            AddScore(3);
        else if (percentageAfterBeat < 80)
            AddScore(2);
        else if (percentageAfterBeat < 100)
            AddScore(1);
    }


    public void StartTimerOnBeat()
    {
        isOnBeat = true;

    }

    public void startLevel()
    {
        
    }

}
