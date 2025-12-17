using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWonMenu : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI scoreText = null;
    [SerializeField]
    private TextMeshProUGUI HighScoreText = null;

    private void Start()
    {
        scoreText.text = "Score: " + GameManager.finalScore.ToString();
        HighScoreText.text = "High Score: " + HighScoreManager.Instance.highScore.ToString();
    }

    public void mainMenu()
    {
        GameManager.Instance.skipToSongSelect = true;
        SceneManager.LoadScene("MainMenu");
    }
}
