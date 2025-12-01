using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody body;

    [SerializeField]
    private Animator thrusterAnimator;

    private float moveDistance = 1f;


    private void Start()
    {
        body = GetComponent<Rigidbody>();
        GameManager.Instance.startedLevel = true;
    }

    private void Update()
    {
        bool isThrusting = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S);

        if (Input.GetKeyDown(KeyCode.A) && GameManager.Instance.isOnBeat == true)
        {
            // movement player left
            body.rotation = Quaternion.Euler(0, -90, 0);
            body.position = new Vector3(body.position.x - moveDistance, body.position.y, body.position.z);

            GameManager.Instance.CheckScore();
        }
        if (Input.GetKeyDown(KeyCode.D) && GameManager.Instance.isOnBeat == true)
        {
            // movement player right
            body.rotation = Quaternion.Euler(0, 90, 0);
            body.position = new Vector3(body.position.x + moveDistance, body.position.y, body.position.z);

            GameManager.Instance.CheckScore();
        }
            
        if (Input.GetKeyDown(KeyCode.W) && GameManager.Instance.isOnBeat == true)
        {
            // movement player up
            body.rotation = Quaternion.Euler(0, 0, 0);
            body.position = new Vector3(body.position.x, body.position.y, body.position.z + moveDistance);

            GameManager.Instance.CheckScore();
        }
            
        if (Input.GetKeyDown(KeyCode.S) && GameManager.Instance.isOnBeat == true)
        {
            // movement player down
            body.rotation = Quaternion.Euler(0, -180, 0);
            body.position = new Vector3(body.position.x, body.position.y, body.position.z - moveDistance);

            GameManager.Instance.CheckScore();
        }
            

        if (thrusterAnimator != null)
            thrusterAnimator.SetBool("Thrusting", isThrusting);
    }
}
