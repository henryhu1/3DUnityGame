using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterController))]
public class PlayerManager : MonoBehaviour, IWeightable
{
    public static PlayerManager Instance;

    public PlayerEventBus Bus { get; private set; }

    private CharacterController controller;
    private PlayerInput playerInputComponent;

    [Header("Player Stats")]
    // @TODO: move to a settings ScriptableObject configuration
    [SerializeField] private float weight = 1;

    [Header("Camera")]
    [SerializeField] private CameraEventChannel cameraEvents;
    [SerializeField] private GameObject headPivot;

    private IInterableObject currentInteractable;

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;

        Bus = new PlayerEventBus();

        playerInputComponent = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
    }

    public void Start()
    {
        cameraEvents.RaiseReturn();
    }

    public void OnEnable()
    {
        playerInputComponent.enabled = true;

        foreach (var comp in GetComponents<IPlayerComponentable>())
            comp.Initialize(this, Bus);
    }

    public void OnDisable()
    {
        playerInputComponent.enabled = false;

        foreach (var comp in GetComponents<IPlayerComponentable>())
            comp.Uninitialize();
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

    public void SwitchHover(IInterableObject newInteractable)
    {
        if (currentInteractable != newInteractable)
        {
            currentInteractable?.OnInteractExit();
            newInteractable.OnInteractHover();
            currentInteractable = newInteractable;
        }
    }

    public void LeaveHover()
    {
        currentInteractable?.OnInteractExit();
        currentInteractable = null;
    }

    public void LookVertically(float pitch)
    {
        headPivot.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    public void InteractWithHovering()
    {
        currentInteractable?.OnInteract();
    }

    public void ApplyDamage(int dmg)
        => Bus.RaiseDamage(dmg);

    public Transform getHeadPivotTransform()
        => headPivot.transform;

    public float GetWeight()
    {
        return weight;
    }
}
