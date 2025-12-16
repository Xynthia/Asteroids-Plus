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

    public List<int> songsBpm;

    public AudioClip deathStar;
    public AudioClip inOrbit;
    public AudioClip hittingTheAtmosphere;

    public AudioClip hitAudio;
    public AudioClip shootAudio;

    [SerializeField] public AudioClip saveSong;
    [SerializeField] public float saveBpm;

    private bool doThisOnce = true;

    private void Awake()
    {
        // setup singleton
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;


        DontDestroyOnLoad(instance);
    }

    void Update()
    {
        if (GameManager.Instance.startedLevel && doThisOnce && BGAudioSource)
        {
            BGAudioSource.clip = bgAudio;
            RhythmManager.Instance.lengthOfSongS = BGAudioSource.clip.length;
            RhythmManager.Instance.bpm = bpm;

            changeVolume(GameManager.Instance.volume);

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
    }


}

