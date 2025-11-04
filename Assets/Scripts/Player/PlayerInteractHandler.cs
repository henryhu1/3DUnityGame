using UnityEngine;

public class PlayerInteractHandler : MonoBehaviour, IPlayerComponentable, IOnPlayerInteract
{
    private PlayerManager _manager;
    private PlayerEventBus _bus;

    public void Initialize(PlayerManager manager, PlayerEventBus bus)
    {
        _manager = manager;
        Debug.Log("interaction initialized");

        _bus = bus;
        _bus.OnInteract += OnPlayerInteract;
    }

    public void Uninitialize()
    {
        _bus.OnInteract -= OnPlayerInteract;
    }

    public void OnPlayerInteract(bool isPressed)
    {
        if (isPressed)
        {
            _manager.InteractWithHovering();
        }
    }
}
