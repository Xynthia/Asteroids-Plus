using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (pauseMenuUI.activeSelf)
            pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {            
            if (gameIsPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
            
        }
    }

    public void resumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        AudioManager.Instance.unPause();
        gameIsPaused = false;
    }

    private void pauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        AudioManager.Instance.pause();
        gameIsPaused = true;
    }

    public void mainMenu()
    {
        resumeGame();
        GameManager.Instance.skipToSongSelect = true;
        SceneManager.LoadScene("MainMenu");
    }
}
