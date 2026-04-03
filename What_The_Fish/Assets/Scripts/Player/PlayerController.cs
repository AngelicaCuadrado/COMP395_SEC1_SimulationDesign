using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Hook hook;
    [SerializeField]
    private float moveSpeed = 5f;

    private PlayerControls controls;
    private Vector2 moveInput;
    public Cache cache { get; private set; }

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        cache = new Cache();
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
    }

    private void MoveHook() 
    {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
        hook.transform.position += move * moveSpeed * Time.deltaTime;
    }
}
