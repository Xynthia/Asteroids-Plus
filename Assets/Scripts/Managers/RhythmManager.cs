using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class RhythmManager : MonoBehaviour
{
    private static RhythmManager instance;
    public static RhythmManager Instance { get { return instance; } }

    [SerializeField] private Beats[] beats;

    [SerializeField] private  UnityEvent startOfMarginTrigger;

    public float beatDureationMs = 0.0f;
    public float beatDureationS = 0.0f;
    public float activeBeat = -1f;

    public float lengthOfSongS = 0.0f;
    public float lengthOfSongMs = 0.0f;
    public float sampleTimeS = 0f;

    public float bpm;


    public float marginDurationMs = 600f;
    public float marginDurationS = 0.0f;
    public float marginTimerS = 0.0f;


    private float nextBeatPosition = 0f;
    public float activeBeatStartPosition = 0f;
    public float activeBeatEndPosition = 0f;

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
        

    }


    private void Update()
    {
        if(doThisOnce && AudioManager.Instance.BGAudioSource)
        {
            lengthOfSongMs = lengthOfSongS * 1000;

            marginDurationS = marginDurationMs / 1000;

            beatDureationS = beats[0].GetBeatLength(bpm);
            beatDureationMs = beatDureationS * 1000;

            doThisOnce = false;
        }

        foreach (Beats beat in beats) {
            
            if (AudioManager.Instance.BGAudioSource)
            {


                sampleTimeS = (AudioManager.Instance.BGAudioSource.timeSamples / (AudioManager.Instance.BGAudioSource.clip.frequency * beatDureationS));
                beat.CheckForNewBeat(sampleTimeS);

                if (sampleTimeS >= activeBeatStartPosition && sampleTimeS <= activeBeatEndPosition)
                {
                    activeBeat = beat.lastBeat;
                    GameManager.Instance.isOnBeat = true;
                }
                else
                {
                    activeBeat = -1;
                    GameManager.Instance.isOnBeat = false;
                    doThisOnce = true;
                }
            }
        }
    }

    public void setNewMargin()
    {
        nextBeatPosition += beatDureationMs;
        activeBeatStartPosition = sampleTimeS - marginDurationS;
        activeBeatEndPosition = sampleTimeS + marginDurationS;

    }


    public float checkPercentagMargin()
    {
        float currentMarginTime = activeBeatEndPosition - sampleTimeS;
        return (currentMarginTime / marginDurationS) * 100;
    }

    public float checkPercentagSong()
    {
        return (sampleTimeS / lengthOfSongS) * 100;
    }

    
}

[System.Serializable]
public class Beats
{
    [SerializeField] private float notesInBeat;
    [SerializeField] private UnityEvent trigger;


    public float lastBeat = 0f;

    public float GetBeatLength(float bpm)
    {
        return 60f / (bpm * notesInBeat);
    }

    public void CheckForNewBeat(float beat)
    {
        if (Mathf.FloorToInt(beat) != lastBeat)
        {
            lastBeat = Mathf.FloorToInt(beat);


            trigger.Invoke();
        }
    }
}
