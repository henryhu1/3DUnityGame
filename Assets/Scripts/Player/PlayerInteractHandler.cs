using System;
using UnityEngine;

public class PlayerInteractHandler : MonoBehaviour, IPlayerComponentable, IOnPlayerInteract
{
    [Header("Settings")]
    [SerializeField] Camera playerCamera;
    [SerializeField] float maxDistance = 5f;
    [SerializeField] LayerMask interactMask;
    [SerializeField] private Transform holdAnchor;
    [SerializeField] private float holdDistance = 1.5f;

    // [Header("Firing Events")]
    // [SerializeField] private InteractableObjectEvent focusEvent;
    public PlayerInteractionState State { get; private set; }
    public Action<bool> OnInteractableHover;
    public IInteractableObject Focused { get; private set; }

    private PlayerManager _manager;
    private PlayerEventBus _bus;
    private IHoldable _heldObject;
    private HoldContext _holdContext;

    public void Initialize(PlayerManager manager, PlayerEventBus bus)
    {
        _manager = manager;

        _bus = bus;
        _bus.OnInteract += OnPlayerInteract;

        State = PlayerInteractionState.Free;
    }

    public void Uninitialize()
    {
        _bus.OnInteract -= OnPlayerInteract;
    }

    private void Update()
    {
        IInteractableObject next = DetectInteractable();

        if (IsPlayerInteractionFreed())
        {
            OnInteractableHover?.Invoke(next != null);
        }

        if (next == null)
        {
            LeaveHover();
        }
        else if (next != Focused)
        {
            SwitchHover(next);
        }

        if (State == PlayerInteractionState.Holding)
        {
            // ScrollHeldObject();
            _heldObject?.OnHoldUpdate(_holdContext);
        }
    }

    private bool IsPlayerInteractionFreed()
    {
        return State == PlayerInteractionState.Free && _heldObject == null;
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

    private InteractionContext CreateInteractionContext()
    {
        return new InteractionContext
        {
            InteractionController = this,
            PlayerCamera = playerCamera,
            HoldAnchor = transform
        };
    }

    private void SwitchHover(IInteractableObject newInteractable)
    {
        if (newInteractable != Focused)
        {
            Focused?.OnHoverExit();
            newInteractable.OnHoverEnter();
            Focused = newInteractable;
            // RaiseFocusChanged(current);
        }
    }

    private void LeaveHover()
    {
        Focused?.OnHoverExit();
        Focused = null;
    }

    private void RaiseFocusChanged()
    {
        // focusEvent.Raise(currentInteractable);
    }

    public void OnPlayerInteract(bool isPressed)
    {
        if (!isPressed) return;

        if (IsPlayerInteractionFreed())
        {
            InteractWithHovering();
        }
        else if (_heldObject != null)
        {
            InteractWithHolding();
        }
    }

    // private void ScrollHeldObject()
    // {
    //     holdDistance += scrollDelta * scrollSpeed;
    //     holdDistance = Mathf.Clamp(holdDistance, minDist, maxDist);
    // }

    private void InteractWithHolding() // Right now just releases held
    {
        if (_heldObject == null) return;

        _heldObject.OnRelease(_holdContext);
        _heldObject = null;
        State = PlayerInteractionState.Free;
    }

    public void PickUp(IHoldable holdable)
    {
        if (holdable == null) return;

        _holdContext = new HoldContext
        {
            HoldAnchor = holdAnchor,
            PlayerCamera = playerCamera,
            // PlayerBody = playerRigidbody,
            // CollisionMask = holdCollisionMask,
            HoldDistance = holdDistance
        };

        _heldObject?.OnRelease(_holdContext);

        holdable.OnPickUp(_holdContext);
        _heldObject = holdable;
        // OnInteractableHover?.Invoke(false)
        State = PlayerInteractionState.Holding;
    }

    public void InteractWithHovering()
    {
        if (Focused == null) return;
        if (!Focused.CanInteract(State)) return;

        Focused?.OnInteract(CreateInteractionContext());
    }
}
