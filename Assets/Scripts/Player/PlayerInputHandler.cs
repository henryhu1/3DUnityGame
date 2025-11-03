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

    public void OnMove(InputValue context)
    {
        _bus.RaiseMove(context.Get<Vector2>());
    }

    public void OnLook(InputValue context)
    {
        _bus.RaiseLook(context.Get<Vector2>());
    }
}
