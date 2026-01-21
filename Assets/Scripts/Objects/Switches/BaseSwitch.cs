using UnityEngine;

public class BaseSwitch : MonoBehaviour, IInteractableObject
{
    [Header("Events")]
    [SerializeField] private SwitchEventChannelSO switchEvent;

    public void OnInteractHover()
    {
    }

    public void OnInteractExit()
    {
    }

    public void OnInteract()
    {
        switchEvent.InvokeToggle();
    }
}
