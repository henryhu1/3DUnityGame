using System;
using UnityEngine;

public class PlayerEventBus
{
    // Example player-scoped events
    public event Action<int> OnDamage;        // damage amount
    public event Action OnDeath;
    public event Action<string> OnItemPickup; // item ID or name
    public event Action<Vector2> OnMove;
    public event Action<Vector2> OnLook;

    // Broadcast methods
    public void RaiseDamage(int amount)
        => OnDamage?.Invoke(amount);

    public void RaiseDeath()
        => OnDeath?.Invoke();

    public void RaiseItemPickup(string item)
        => OnItemPickup?.Invoke(item);

    public void RaiseMove(Vector2 direction)
        => OnMove?.Invoke(direction);

    public void RaiseLook(Vector2 direction)
        => OnLook?.Invoke(direction);
}
