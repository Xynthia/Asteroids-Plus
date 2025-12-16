using UnityEngine;
using UnityEngine.UI;

public class LengthSongUI : MonoBehaviour
{
    [SerializeField] Slider currentSongProgressBar;


    // Update is called once per frame
    void Update()
    {
        currentSongProgressBar.value = (RhythmManager.Instance.checkPercentagSong());

    }

    
}
