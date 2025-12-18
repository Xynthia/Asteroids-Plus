using System.Collections;
using UnityEngine;

public class CircleManager : MonoBehaviour
{
    private Vector3 maxSize;
    private Vector3 halfSize;
    private Vector3 minSize;

    private float scale = 3f;
    private float timeToScale = 0.000f;
    private float timeToWait = 0.000f;


    private Color transparent = new Color(0f, 0f, 0f, 0f);

    void Start()
    {

        halfSize = transform.localScale;

        maxSize = halfSize * scale;

        minSize = halfSize / scale;


        timeToWait = ((10.0f / 100.0f) * RhythmManager.Instance.marginDurationS) * 2;

        timeToScale = (RhythmManager.Instance.beatDureationS - RhythmManager.Instance.marginDurationS) + timeToWait;

        StartCoroutine(startAnimation());
    }

    IEnumerator startAnimation()
    {

        yield return StartCoroutine(animationPartOne());

        yield return StartCoroutine(animationPartTwo());

        Destroy(gameObject);
    }

    IEnumerator animationPartOne()
    {
        StartCoroutine(changeScale( maxSize, halfSize, timeToScale));
        StartCoroutine(changeTransparency(transparent, Color.white, timeToScale));

        yield return new WaitForSeconds(timeToScale + timeToWait);
    }

    IEnumerator animationPartTwo()
    {
        StartCoroutine(changeScale(halfSize, minSize, timeToScale));
        StartCoroutine(changeTransparency(Color.white, transparent, timeToScale));

        yield return new WaitForSeconds(timeToScale + timeToWait);
    }



    IEnumerator changeTransparency(Color start, Color end, float duration)
    {
        SpriteRenderer spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            spriteRenderer.color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        spriteRenderer.color = end;
    }

    IEnumerator changeScale(Vector3 start, Vector3 end, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            transform.localScale = Vector3.Lerp(start, end, normalizedTime);
            yield return null;
        }
        transform.localScale = end;
    }
}
