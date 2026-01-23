using UnityEngine;

public struct HoldContext
{
    public Transform HoldAnchor;

    public float HoldDistance;

    public Camera PlayerCamera;
    public Rigidbody PlayerBody;
    public LayerMask CollisionMask;
}
