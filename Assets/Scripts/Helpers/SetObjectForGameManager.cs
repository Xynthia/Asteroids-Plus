using TMPro;
using UnityEngine;

public class SetObjectForGameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTextObject;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI visualScoreOnBeat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.player = player;
        GameManager.Instance.scoreTextObject = scoreTextObject;
        GameManager.Instance.playerMovement = player.GetComponent<PlayerMovement>();
        GameManager.Instance.visualScoreOnBeat = visualScoreOnBeat;
    }

}
