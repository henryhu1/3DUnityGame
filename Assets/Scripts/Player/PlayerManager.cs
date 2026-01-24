using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterController))]
public class PlayerManager : MonoBehaviour, IWeightable
{
    public PlayerEventBus Bus { get; private set; }

    private CharacterController controller;
    private PlayerInput playerInputComponent;

    [Header("Player Stats")]
    // @TODO: move to a settings ScriptableObject configuration
    [SerializeField] private float weight = 1;

    [Header("Actionables")]
    [SerializeField] private Light flashlight;

    [Header("Camera")]
    [SerializeField] private CameraEventChannel cameraEvents;
    [SerializeField] private GameObject headPivot;

    private void Awake()
    {
        Bus = new PlayerEventBus();

        playerInputComponent = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        cameraEvents.RaiseReturn();
    }

    private void OnEnable()
    {
        playerInputComponent.enabled = true;

        Bus.OnFlashlightToggle += ToggleFlashlight;
        foreach (var comp in GetComponents<IPlayerComponentable>())
            comp.Initialize(this, Bus);
    }

    private void OnDisable()
    {
        playerInputComponent.enabled = false;

        Bus.OnFlashlightToggle -= ToggleFlashlight;
        foreach (var comp in GetComponents<IPlayerComponentable>())
            comp.Uninitialize();
    }

    private void ToggleFlashlight(bool isPressed)
    {
        if (isPressed) return;

        flashlight.enabled = !flashlight.enabled;
    }

    public void MovePlayer(Vector3 velocity)
    {
        controller.Move(Time.deltaTime * velocity);
        transform.position = controller.transform.position;
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, controller.height / 2f + 0.2f);
    }

    public void LookVertically(float pitch)
    {
        headPivot.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    public void ApplyDamage(int dmg)
        => Bus.RaiseDamage(dmg);

    public float GetWeight()
    {
        return weight;
    }
}
