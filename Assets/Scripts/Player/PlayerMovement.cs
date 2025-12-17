using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody body;

    public Vector3 newPosition;

    [SerializeField]
    private Animator thrusterAnimator;

    private float moveDistance = 1f;
    private float moveTime = 1f;

    private Quaternion newRotation;
    
    private bool isThrusting = false;


    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && GameManager.Instance.isOnBeat && !GameManager.Instance.pressedOnceOnBeat)
        {
            isThrusting = true;

            // movement player left
            newRotation = Quaternion.Euler(0, -90, 0);
            newPosition = new Vector3(body.position.x - moveDistance, body.position.y, body.position.z);

            GameManager.Instance.playerPressedButton();
        }
        if (Input.GetKeyDown(KeyCode.D) && GameManager.Instance.isOnBeat && !GameManager.Instance.pressedOnceOnBeat)
        {
            isThrusting = true;

            // movement player right
            newRotation = Quaternion.Euler(0, 90, 0);
            newPosition = new Vector3(body.position.x + moveDistance, body.position.y, body.position.z);

            GameManager.Instance.playerPressedButton();
        }
        if (Input.GetKeyDown(KeyCode.W) && GameManager.Instance.isOnBeat && !GameManager.Instance.pressedOnceOnBeat)
        {
            isThrusting = true;

            // movement player up
            newRotation = Quaternion.Euler(0, 0, 0);
            newPosition = new Vector3(body.position.x, body.position.y, body.position.z + moveDistance);

            GameManager.Instance.playerPressedButton();
        }
        if (Input.GetKeyDown(KeyCode.S) && GameManager.Instance.isOnBeat && !GameManager.Instance.pressedOnceOnBeat)
        {
            isThrusting = true;

            // movement player down
            newRotation = Quaternion.Euler(0, -180, 0);
            newPosition = new Vector3(body.position.x, body.position.y, body.position.z - moveDistance);

            
            GameManager.Instance.playerPressedButton();
        }


        if (thrusterAnimator != null)
            thrusterAnimator.SetBool("Thrusting", isThrusting);

    }

    public void move()
    {
        StartCoroutine(changeRotation(body.rotation, newRotation, RhythmManager.Instance.marginDurationS));
        StartCoroutine(changePosition(body.position, newPosition, RhythmManager.Instance.marginDurationS));
    }

    IEnumerator changeRotation(Quaternion start, Quaternion end, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            body.rotation = Quaternion.Lerp(start, end, normalizedTime);
            yield return null;
        }
        body.rotation = end;
        isThrusting = false;
    }

    IEnumerator changePosition(Vector3 start, Vector3 end, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            body.position = Vector3.Lerp(start, end, normalizedTime);
            yield return null;
        }
        body.position = end;
        isThrusting = false;
    }

}
