using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float forceFactor = 5.0f;

    private InputAction moveAction;
    private Rigidbody rb;
    private AudioSource hit1Sound;


    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
        hit1Sound = GetComponent<AudioSource>();
        GameState.Subscribe(OnEffectsVolumeChanged,
            nameof(GameState.effectsVolume),
            nameof(GameState.isMuted));
        OnEffectsVolumeChanged();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if ( ! hit1Sound.isPlaying)
            {
                hit1Sound.Play();
            }
            
        }
    }

    private void OnEffectsVolumeChanged()
    {
        hit1Sound.volume = GameState.isMuted ? 0.0f : GameState.effectsVolume;
    }

    private void OnDestroy()
    {
        GameState.UnSubscribe(OnEffectsVolumeChanged,
            nameof(GameState.effectsVolume),
            nameof(GameState.isMuted));
    }
}
