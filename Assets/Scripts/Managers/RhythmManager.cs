using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RhythmManager : MonoBehaviour
{
    private static RhythmManager instance;
    public static RhythmManager Instance { get { return instance; } }

    [SerializeField] private float bpm;
    [SerializeField] private AudioSource bgAudioSource;
    [SerializeField] private float marginPercentage = 33.3f;

    public float beatDureationMs = 0.0f;

    public float activeBeat = -1f;

    public float lengthOfSongS = 0.0f;
    public float lengthOfSongMs = 0.0f;
    public float timePositionMs = 0f;

    public float marginDurationMs = 0.0f;
    public float marginDurationS = 0.0f;
    public float marginTimerS = 0.0f;


    [SerializeField] UnityEvent onBeatTrigger;

    private float lastBeat = 0f;
    private float nextBeatPosition = 0f;
    private float activeBeatStartPosition = 0f;
    private float activeBeatEndPosition = 0f;

    private bool doThisOnce = true;

    private void Awake()
    {
        // setup singleton
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;

    }

    void Start()
    {
        lengthOfSongS = bgAudioSource.clip.length;
        lengthOfSongMs = lengthOfSongS * 1000;
        
        beatDureationMs = (60 / bpm) * 1000;

        marginDurationMs = 300;
        marginDurationS = marginDurationMs / 1000;

        nextBeatPosition = beatDureationMs;

    }

    // Update is called once per frame
    void Update()
    {

        float position = checkPositionMsSong();

        if (position >= nextBeatPosition)
        {
            lastBeat += 1;
            nextBeatPosition += beatDureationMs;
            activeBeatStartPosition = nextBeatPosition - marginDurationMs;
            activeBeatEndPosition = nextBeatPosition + marginDurationMs;
        }

        if (position >= activeBeatStartPosition && position <= activeBeatEndPosition)
        {
            activeBeat = lastBeat;

            if (doThisOnce)
            {
                onBeatTrigger.Invoke();
                doThisOnce = false;
            }
        }
        else
        {
            activeBeat = -1;
            doThisOnce = true;
        }
    }

    public float checkPositionMsSong()
    {
        return timePositionMs = bgAudioSource.time * 1000;
    }

    public float checkPercentagMargin()
    {
        float currentMarginTime = timePositionMs - activeBeatStartPosition;
        return currentMarginTime / marginDurationMs * 100;
    }

    public void unPause()
    {
        bgAudioSource.UnPause();
    }

    public void pause()
    {
        bgAudioSource.Pause();
    }

    public void changeVolume(float volume)
    {
        bgAudioSource.volume = volume;
    }

    
}
