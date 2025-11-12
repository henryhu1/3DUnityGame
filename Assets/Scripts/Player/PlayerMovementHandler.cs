using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour, IPlayerComponentable, IOnPlayerMove
{
    private PlayerManager _manager;
    private PlayerEventBus _bus;

    // Position
    private Vector2 movement;
    private Vector2 velocity;
    private Vector3 velocity3D = Vector3.zero;

    // Rotation
    private float pitch = 0;
    private Vector2 lookInput;
    const float PITCH_CLAMP = 60;

    // @TODO: move to a settings ScriptableObject configuration
    [SerializeField] private float speed = 3;
    [SerializeField] private float sensitivity = 0.5f;

    public void Initialize(PlayerManager manager, PlayerEventBus bus)
    {
        _manager = manager;

        _bus = bus;
        _bus.OnMove += OnPlayerMove;
        _bus.OnLook += OnPlayerLook;
    }

    public void Uninitialize()
    {
        _bus.OnMove -= OnPlayerMove;
        _bus.OnLook -= OnPlayerLook;
    }

    public void OnPlayerMove(Vector2 direction)
    {
        movement = direction;
    }

    private void OnPlayerLook(Vector2 direction)
    {
        lookInput = direction;
    }

    private void FixedUpdate()
    {
        Vector3 direction = transform.forward * movement.y + transform.right * movement.x;
        velocity3D = direction * speed;

        // Vector2 distance = Time.deltaTime * velocity;
        // Vector3 forwardDistance = transform.forward * distance.y;
        // Vector3 sidewaysDistance = transform.right * distance.x;
        // transform.position += forwardDistance + sidewaysDistance;
    }

    private void Update()
    {
        _manager.MovePlayer(velocity3D);

        pitch = Mathf.Clamp(pitch - lookInput.y * sensitivity, -PITCH_CLAMP, PITCH_CLAMP);
        _manager.LookVertically(pitch);
        transform.Rotate(0,  lookInput.x * sensitivity, 0);
    }
}
