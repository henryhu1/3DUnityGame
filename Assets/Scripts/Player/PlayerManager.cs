using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public PlayerEventBus Bus { get; private set; }

    private PlayerInput playerInputComponent;

    [Header("Camera")]
    [SerializeField] private CameraEventChannel cameraEvents;
    [SerializeField] private GameObject headPivot;

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;

        Bus = new PlayerEventBus();

        playerInputComponent = GetComponent<PlayerInput>();
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

    public void LookVertically(float pitch)
    {
        headPivot.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    public void ApplyDamage(int dmg)
        => Bus.RaiseDamage(dmg);

    public Transform getHeadPivotTransform()
        => headPivot.transform;
}
