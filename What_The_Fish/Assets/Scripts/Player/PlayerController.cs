using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Hook hook;
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private AudioSource movementAudioSource;

    public float minX = -2.55f;
    public float maxX = 3.40f;

    private PlayerControls controls;
    private Vector2 moveInput;
    public Cache cache;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        if (cache == null) cache = new Cache();
        hook.SetPlayer(this);
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void Update()
    {
        MoveHook();
        HandleMovementAudio();
    }

    private void MoveHook()
    {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
        hook.transform.localPosition += move * moveSpeed * Time.deltaTime;

        Vector3 localPos = hook.transform.localPosition;
        localPos.x = Mathf.Clamp(localPos.x, minX, maxX);
        hook.transform.localPosition = localPos;
    }

    private void HandleMovementAudio()
    {
        if (movementAudioSource == null) return;

        if (moveInput != Vector2.zero)
        {
            if (!movementAudioSource.isPlaying)
            {
                movementAudioSource.Play();
            }
        }
        else
        {
            if (movementAudioSource.isPlaying)
            {
                movementAudioSource.Stop();
            }
        }
    }
}