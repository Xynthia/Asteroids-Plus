using UnityEngine;
using UnityEngine.UI;

public class LengthSongUI : MonoBehaviour
{
    [SerializeField] Slider currentSongProgressBar;


    // Update is called once per frame
    void Update()
    {
        if (RhythmManager.Instance)
        {
            float newValue = RhythmManager.Instance.checkPercentagSong();

            currentSongProgressBar.value = newValue;
        }

    }

    
}
