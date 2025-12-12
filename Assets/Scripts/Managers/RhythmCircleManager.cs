using UnityEngine;

public class RhythmCircleManager: MonoBehaviour
{
    [SerializeField] private GameObject circleVisual;
    [SerializeField] private float maxSize = 1f;
    private Vector3 minSize;

    private float timeToScale = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        minSize = transform.localScale;
        timeToScale = RhythmManager.Instance.marginDurationS / 2;
        print(timeToScale + " " + Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, minSize, timeToScale);

        if (GameManager.Instance.isOnBeat)
        {
            circleVisual.SetActive(true);
        }
        else
        {
            circleVisual.SetActive(false);
        }
    }

    public void setMaxSize()
    {
        transform.localScale = minSize * maxSize;
    }
}
