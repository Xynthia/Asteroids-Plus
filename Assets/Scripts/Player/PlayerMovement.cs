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
    private float moveSpeed = 6f;

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
            GameManager.Instance.pressedOnceOnBeat = true;

            // movement player left
            newRotation = Quaternion.Euler(0, -90, 0);
            newPosition = new Vector3(body.position.x - moveDistance, body.position.y, body.position.z);

            GameManager.Instance.CheckScore();
        }
        else if (Input.GetKeyDown(KeyCode.D) && GameManager.Instance.isOnBeat && !GameManager.Instance.pressedOnceOnBeat)
        {
            isThrusting = true;
            GameManager.Instance.pressedOnceOnBeat = true;

            // movement player right
            newRotation = Quaternion.Euler(0, 90, 0);
            newPosition = new Vector3(body.position.x + moveDistance, body.position.y, body.position.z);

            GameManager.Instance.CheckScore();
        }
        else if (Input.GetKeyDown(KeyCode.W) && GameManager.Instance.isOnBeat && !GameManager.Instance.pressedOnceOnBeat)
        {
            isThrusting = true;
            GameManager.Instance.pressedOnceOnBeat = true;

            // movement player up
            newRotation = Quaternion.Euler(0, 0, 0);
            newPosition = new Vector3(body.position.x, body.position.y, body.position.z + moveDistance);

            GameManager.Instance.CheckScore();
        }
        else if (Input.GetKeyDown(KeyCode.S) && GameManager.Instance.isOnBeat && !GameManager.Instance.pressedOnceOnBeat)
        {
            isThrusting = true;
            GameManager.Instance.pressedOnceOnBeat = true;

            // movement player down
            newRotation = Quaternion.Euler(0, -180, 0);
            newPosition = new Vector3(body.position.x, body.position.y, body.position.z - moveDistance);

            GameManager.Instance.CheckScore();
        }
        else
        {
            isThrusting = false;
        }


        body.rotation = Quaternion.Lerp(body.rotation, newRotation, Time.deltaTime * moveSpeed);
        body.position = Vector3.Lerp(body.position, newPosition, Time.deltaTime * moveSpeed);

        if (thrusterAnimator != null)
            thrusterAnimator.SetBool("Thrusting", isThrusting);
    }

}
