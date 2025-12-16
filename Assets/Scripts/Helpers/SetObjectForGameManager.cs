using TMPro;
using UnityEngine;

public class SetObjectForManagers : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTextObject;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI visualScoreOnBeat;
    [SerializeField] private AudioSource bgAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.player = player;
        GameManager.Instance.scoreTextObject = scoreTextObject;
        GameManager.Instance.playerMovement = player.GetComponent<PlayerMovement>();
        GameManager.Instance.visualScoreOnBeat = visualScoreOnBeat;

        AudioManager.Instance.BGAudioSource = bgAudioSource;
        AudioManager.Instance.SFXAudioSource = sfxAudioSource;
    }

}
