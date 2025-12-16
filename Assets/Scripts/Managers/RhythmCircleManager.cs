using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmCircleManager: MonoBehaviour
{
    private static RhythmCircleManager instance;
    public static RhythmCircleManager Instance { get { return instance; } }

    [SerializeField] private GameObject circlePrefab;

    public void spawn()
    {
            GameObject circle = Instantiate(circlePrefab, transform.position, Quaternion.identity, transform);
            circle.transform.Rotate(90, 0, 0);
    }

    
}
