using System.Collections;
using TMPro;
using UnityEngine;


public class EventsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialog;

    private float scale = 1.2f;

    private string dialogOne = "This game works on a rhythm!\r\nTry and see if you can move~\r\nuse W, A, S,D";
    private string dialogTwo = "But of course this is asteroids~\r\nYou shoot when you move\r\n!Keep in mind!\r\nMovement gives you the most points";
    private string dialogThree = "Every action you take is based\r\non how well you time it.\r\nbetter rhythm == beter score!";
    private string dialogFour = "Asteroids... \r\nI TOTALLY FORGOT THE ASTEROIDS!\r\nShoot them get extra points~";


    private bool eventOneDone = false;
    private bool w = false;
    private bool a = false;
    private bool s = false;
    private bool d = false;

    private bool eventTwoDone = false;
    private float shootCounter = 0f;

    private bool eventThreeDone = false;
    private float actionCounter = 0f;

    private float eventFourTimer = 0f;


    private void Start()
    {
        dialog.text = dialogOne;
    }

    private void Update()
    {
        // showing movement on beat.

        if (Input.GetKeyDown(KeyCode.W) && GameManager.Instance.isOnBeat)
            w = true;
        if (Input.GetKeyDown(KeyCode.A) && GameManager.Instance.isOnBeat)
            a = true;
        if (Input.GetKeyDown(KeyCode.S) && GameManager.Instance.isOnBeat)
            s = true;
        if (Input.GetKeyDown(KeyCode.D) && GameManager.Instance.isOnBeat)
            d = true;

        if (w && a && s && d)
            eventOneDone = true;

        if (eventOneDone)
        {
            dialog.text = dialogTwo;
            
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) && GameManager.Instance.isOnBeat)
                shootCounter += 1;

            if (shootCounter == 10)
                eventTwoDone = true;
        }
        
        if (eventTwoDone)
        {
            dialog.text = dialogThree;

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) && GameManager.Instance.isOnBeat)
                actionCounter += 1;

            if (actionCounter == 10)
                eventThreeDone = true;
        }

        if (eventThreeDone)
        {
            dialog.text = dialogFour;

            AsteroidManager.Instance.setMaxAsteroids(2);
            eventFourTimer += Time.deltaTime;

            if (eventFourTimer >= 5)
            {
                dialog.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator animation()
    {
            Vector3 begin = dialog.transform.localScale * 0.9f;
            Vector3 mid = dialog.transform.localScale * 1.25f;
            Vector3 end = dialog.transform.localScale * 1f;
            yield return StartCoroutine(changeScale(begin, mid, 0.2f));
            StartCoroutine(changeScale(mid, end, 0.2f));
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


