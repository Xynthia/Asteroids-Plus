using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance {  get { return instance; } }

    [SerializeField] public AudioSource BGAudioSource;
    [SerializeField] public AudioSource SFXAudioSource;


    [SerializeField] public float bpm;
    [SerializeField] public AudioClip bgAudio;


    public float volume = 0.5f;

    [SerializeField] public List<SongData> songs;

    public SongData deathStar;
    public SongData inOrbit;
    public SongData hittingTheAtmosphere;

    public AudioClip hitAudio;
    public AudioClip shootAudio;

    public bool doThisOnce = true;

    private void Awake()
    {
        // setup singleton
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }

    }

    private void Start()
    {
        foreach(SongData song in songs)
        {
            if (song.name == "Death Star")
            {
                deathStar = song;
            }
            if (song.name == "In Orbit")
            {
                inOrbit = song;
            }
            if (song.name == "Hitting the Atmosphere")
            {
                hittingTheAtmosphere = song;
            }
            
        }
    }

    void Update()
    {
        if (GameManager.Instance.startedLevel && doThisOnce && BGAudioSource)
        {
            BGAudioSource.clip = bgAudio;
            RhythmManager.Instance.lengthOfSongS = BGAudioSource.clip.length;
            RhythmManager.Instance.bpm = bpm;

            changeVolume(volume);

            BGAudioSource.Play();

            doThisOnce = false;
        }
    }

    public void playSFX(AudioClip SFX)
    {
        SFXAudioSource.PlayOneShot(SFX);
    }

    public void unPause()
    {
        BGAudioSource.UnPause();
    }

    public void pause()
    {
        BGAudioSource.Pause();
    }

    public void changeVolume(float volume)
    {
        BGAudioSource.volume = volume;
        SFXAudioSource.volume = volume;
    }


}

[System.Serializable]
public class SongData
{
    public string name;
    public AudioClip song;
    public float bpm;
    public float notesInBeat;
    public float marginDurationMs;
}