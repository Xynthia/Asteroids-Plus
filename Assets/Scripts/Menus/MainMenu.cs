using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] Slider soundSlider;
    [SerializeField] AudioSource audioSource;

   
    public void startGame()
    {
        GameManager.Instance.startedLevel = true;
        SceneManager.LoadScene("Level1");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void back()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void changedVolume()
    {
        float volume = soundSlider.value / 100;
        GameManager.Instance.volume = volume;
        audioSource.volume = volume;
        StartCoroutine(playClip());
    }

    IEnumerator playClip()
    {
        audioSource.Play();
        yield return new WaitForSeconds(5);
        audioSource.Stop();
    }
}
