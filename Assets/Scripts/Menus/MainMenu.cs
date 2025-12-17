using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject songSelectMenu;
    [SerializeField] Slider soundSlider;
    [SerializeField] AudioSource audioSource;

    private void Update()
    {
        
        if (GameManager.Instance.skipToSongSelect)
        {
            songSelect();
        }
    }


    private void startGame()
    {
        GameManager.Instance.startedLevel = true;
        SceneManager.LoadScene("Level1");
    }

    public void deathStar()
    {
        AudioManager.Instance.bgAudio = AudioManager.Instance.deathStar.song;

        GameManager.Instance.startedLevel = true;
        SceneManager.LoadScene("Tutorial");
    }

    public void inOrbit()
    {
        AudioManager.Instance.bgAudio = AudioManager.Instance.inOrbit.song;
        
        startGame();
    }

    public void hittingTheAtmosphere()
    {
        AudioManager.Instance.bgAudio = AudioManager.Instance.hittingTheAtmosphere.song;
        
        startGame();
    }

    public void songSelect()
    {
        songSelectMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void changedVolume()
    {
        float volume = soundSlider.value / 100;
        AudioManager.Instance.volume = volume;
        audioSource.volume = volume;
        StartCoroutine(playClip());
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void back()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        songSelectMenu.SetActive(false);

    }

    IEnumerator playClip()
    {
        audioSource.Play();
        yield return new WaitForSeconds(5);
        audioSource.Stop();
    }
}
