using UnityEngine;

public class BlackoutBall : MonoBehaviour, IHoldable, IInteractableObject
{
    [SerializeField] Collider[] colliders;

    private Rigidbody _rb;

    private bool isHeld;
    private int originalLayer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        originalLayer = gameObject.layer;
    }

    public void OnPickUp(HoldContext context)
    {
        isHeld = true;
        gameObject.layer = LayerMask.NameToLayer("HeldObject");

        _rb.isKinematic = true;
        _rb.useGravity = false;

        foreach (var col in colliders)
            col.enabled = false;
    }

    public void OnRelease(HoldContext context)
    {
        isHeld = false;
        gameObject.layer = originalLayer;

        foreach (var col in colliders)
            col.enabled = true;

        _rb.isKinematic = false;
        _rb.useGravity = true;
    }

    public void OnHoldUpdate(HoldContext context)
    {
        transform.SetPositionAndRotation(Vector3.Lerp(
            transform.position,
            context.HoldAnchor.position,
            Time.deltaTime * 20f
        ), Quaternion.Slerp(
            transform.rotation,
            context.HoldAnchor.rotation,
            Time.deltaTime * 20f
        ));
    }

    public void OnHoverEnter()
    {
    }

    public void OnHoverExit()
    {
    }

    public void OnInteract(InteractionContext ctx)
    {
        if (!isHeld)
        {
            ctx.InteractionController.PickUp(this);
        }
    }

    public bool CanInteract(PlayerInteractionState state)
    {
        return state == PlayerInteractionState.Free;
    }
}
