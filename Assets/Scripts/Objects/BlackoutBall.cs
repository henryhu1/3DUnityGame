using UnityEngine;

public class BlackoutBall : MonoBehaviour, IHoldable
{
    [SerializeField] Collider[] colliders;

    [Header("Settings")]
    [SerializeField] float holdDistance = 2.5f;
    [SerializeField] float followSpeed = 15f;

    public Rigidbody Rigidbody { get; private set; }

    bool isHeld;
    PlayerInteractHandler holder;

    int originalLayer;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        originalLayer = gameObject.layer;
    }

    private void LateUpdate()
    {
        if (!isHeld) return;

        Transform cam = holder.CameraTransform;

        transform.position =
            cam.position + cam.forward * holdDistance;
    }

    void Pickup(PlayerInteractHandler interactor)
    {
        isHeld = true;
        holder = interactor;

        Rigidbody.isKinematic = true;
        Rigidbody.useGravity = false;

        foreach (var col in colliders)
            col.enabled = false;
    }

    private void Release()
    {
        isHeld = false;
        holder = null;

        foreach (var col in colliders)
            col.enabled = true;

        Rigidbody.isKinematic = false;
        Rigidbody.useGravity = true;
    }

    public void OnHoverEnter()
    {
    }

    public void OnHoverExit()
    {
    }

    public void OnInteract(PlayerInteractHandler interactor)
    {
        if (isHeld)
        {
            Release();
        }
        else
        {
            Pickup(interactor);
        }
    }
}
