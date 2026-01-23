using UnityEngine;

public class BaseSwitch : MonoBehaviour, IInteractableObject
{
    [Header("Events")]
    [SerializeField] private SwitchEventChannelSO switchEvent;

    public void OnHoverEnter()
    {
    }

    public void OnHoverExit()
    {
    }

    public virtual void OnInteract()
    {
        switchEvent.InvokeToggle();
    }

    public bool CanInteract(PlayerInteractionState state)
    {
        return state == PlayerInteractionState.Free;
    }
}
