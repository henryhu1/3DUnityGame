using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour, IPlayerComponentable
{
    private PlayerEventBus _bus;

    public void Initialize(PlayerManager manager, PlayerEventBus bus)
    {
        _bus = bus;
    }

    public void Uninitialize()
    {

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _bus.RaiseMove(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _bus.RaiseLook(context.ReadValue<Vector2>());
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        _bus.RaiseInteract(context.ReadValueAsButton());
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        _bus.RaiseFlashlightToggle(context.ReadValueAsButton());
    }
}
