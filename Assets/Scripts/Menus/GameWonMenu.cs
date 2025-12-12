using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWonMenu : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI scoreText = null;

    private void Start()
    {
        scoreText.text = "Score: " + GameManager.finalScore.ToString();
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
