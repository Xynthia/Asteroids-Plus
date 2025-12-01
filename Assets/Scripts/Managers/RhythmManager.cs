using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RhythmManager : MonoBehaviour
{
    private static RhythmManager instance;
    public static RhythmManager Instance { get { return instance; } }

    [SerializeField] private float _bpm;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Intervals[] _intervals;
    [SerializeField] private float percentageSafeTime = 33.3f;

    private float safeTime;

    private void Awake()
    {
        // setup singleton
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;

    }

    void Start()
    {
        safeTime =  _intervals[0].GetIntervalLength(_bpm) * (percentageSafeTime / 100);
        GameManager.Instance.safeTime = safeTime;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Intervals interval in _intervals)
        {
            float sampledTime = (_audioSource.timeSamples / (_audioSource.clip.frequency * interval.GetIntervalLength(_bpm)));
            interval.CheckForNewInterval(sampledTime);
        }
    }

}

[System.Serializable]
public class Intervals
{
    [SerializeField] private float _steps;
    [SerializeField] private UnityEvent _trigger;
    private int _lastInterval;

    public float GetIntervalLength(float bpm)
    {
        return 60f / (bpm * _steps);
    }

    public void CheckForNewInterval(float interval)
    {
        if (Mathf.FloorToInt(interval) != _lastInterval)
        {
            _lastInterval = Mathf.FloorToInt(interval);
            _trigger.Invoke();
        }
    }
}
