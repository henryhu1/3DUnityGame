using System;
using UnityEngine;

public class PlayerInteractHandler : MonoBehaviour, IPlayerComponentable, IOnPlayerInteract
{
    [Header("Settings")]
    [SerializeField] Camera playerCamera;
    [SerializeField] float maxDistance = 5f;
    [SerializeField] LayerMask interactMask;

    // [Header("Firing Events")]
    // [SerializeField] private InteractableObjectEvent focusEvent;
    public Action<bool> OnInteractableHover;

    public Transform CameraTransform => playerCamera.transform;

    private PlayerManager _manager;
    private PlayerEventBus _bus;

    private IInteractableObject currentInteractable;

    public void Initialize(PlayerManager manager, PlayerEventBus bus)
    {
        _manager = manager;

        _bus = bus;
        _bus.OnInteract += OnPlayerInteract;
    }

    public void Uninitialize()
    {
        _bus.OnInteract -= OnPlayerInteract;
    }

    private void Update()
    {
        IInteractableObject next = DetectInteractable();

        OnInteractableHover?.Invoke(next != null);

        if (next == null)
        {
            LeaveHover();
        }
        else if (next != currentInteractable)
        {
            SwitchHover(next);
        }
    }

    private IInteractableObject DetectInteractable()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactMask))
        {
            GameObject hitObj = hit.collider.gameObject;
            if (hitObj.TryGetComponent(out IInteractableObject interactable))
            {
                return interactable;
            }
        }
        return null;
    }

    private void SwitchHover(IInteractableObject newInteractable)
    {
        if (newInteractable != currentInteractable)
        {
            currentInteractable?.OnHoverExit();
            newInteractable.OnHoverEnter();
            currentInteractable = newInteractable;
            // RaiseFocusChanged(current);
        }
    }

    private void LeaveHover()
    {
        currentInteractable?.OnHoverExit();
        currentInteractable = null;
    }

    void RaiseFocusChanged()
    {
        // focusEvent.Raise(currentInteractable);
    }

    public void OnPlayerInteract(bool isPressed)
    {
        if (isPressed)
        {
            InteractWithHovering();
        }
    }

    public void InteractWithHovering()
    {
        currentInteractable?.OnInteract(this);
    }
}
