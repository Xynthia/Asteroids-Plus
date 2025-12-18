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


    public float beatDureationMs = 0.0f;
    public float beatDureationS = 0.0f;
    public float activeBeat = -1f;

    public float lengthOfSongS = 0.0f;
    public float lengthOfSongMs = 0.0f;
    public float sampleTimeS = 0f;

    public float bpm;


    public float marginDurationMs = 600f;
    public float marginDurationS = 0.0f;


    public float nextBeatPosition = 0f;
    public float activeBeatStartPosition = 0f;
    public float activeBeatEndPosition = 0f;



    private void Awake()
    {
        // setup singleton
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;

    }

    void Start()
    {
        foreach (SongData songData in AudioManager.Instance.songs)
        {
            if (AudioManager.Instance.bgAudio == songData.song)
            {
                marginDurationMs = songData.marginDurationMs;
                marginDurationS = marginDurationMs / 1000;

                bpm = songData.bpm;
                beats[0].notesInBeat = songData.notesInBeat;
                beatDureationS = beats[0].GetBeatLength(bpm);
                beatDureationMs = beatDureationS * 1000;
                nextBeatPosition += beatDureationS;

                lengthOfSongS = songData.song.length;
                lengthOfSongMs = lengthOfSongS * 1000;

                
            }
        }
        
    }


    private void Update()
    {
        foreach (Beats beat in beats) {
            if (AudioManager.Instance.BGAudioSource && GameManager.Instance.startedLevel)
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
                }
            }
        }

        
    }

    public void setNewMargin()
    {
        nextBeatPosition = sampleTimeS + beatDureationS;
        activeBeatStartPosition = nextBeatPosition - marginDurationS;
        activeBeatEndPosition = nextBeatPosition + marginDurationS;
    }


    public float checkPercentagMargin()
    {
        float deel = sampleTimeS - activeBeatStartPosition;
        float geheel = activeBeatEndPosition - activeBeatStartPosition;
        return (deel / geheel) * 100;
    }

    public float checkPercentagSong()
    {
        return (sampleTimeS / lengthOfSongS) * 100;
    }

    
}

[System.Serializable]
public class Beats
{
    [SerializeField] public float notesInBeat;
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
