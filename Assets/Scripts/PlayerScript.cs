using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float forceFactor = 5.0f;

    private InputAction moveAction;
    private Rigidbody rb;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 correctedForward = Camera.main.transform.forward;
        correctedForward.y = 0.0f;
        correctedForward.Normalize();
        Vector3 forceValue = forceFactor *
            // new Vector3(moveValue.x, 0.0f, moveValue.y); - відносно "Світу"
            (  // залежно від повороту камери
            Camera.main.transform.right * moveValue.x +
            correctedForward * moveValue.y
            );
        rb.AddForce(forceValue);
    }
}
