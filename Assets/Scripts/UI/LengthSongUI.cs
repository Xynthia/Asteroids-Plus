using UnityEngine;
using UnityEngine.UI;

public class LengthSongUI : MonoBehaviour
{
    [SerializeField] Slider currentSongProgressBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSongProgressBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSongProgressBar.value = (RhythmManager.Instance.timePositionMs/1000);

    }

    
}
