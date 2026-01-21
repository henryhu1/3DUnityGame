using UnityEngine;

public interface IHoldable : IInteractableObject
{
    Rigidbody Rigidbody { get; }
}
