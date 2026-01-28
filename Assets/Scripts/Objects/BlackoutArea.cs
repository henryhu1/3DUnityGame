using UnityEngine;

public class BlackoutArea : MonoBehaviour
{
    [SerializeField] private LayerMask detectableLayers;

    private void OnTriggerEnter(Collider other)
    {
        IBlackoutable blackoutable = DetectBlackoutableObject(other);

        if (blackoutable == null) return;

        blackoutable.EnterField();
    }

    private void OnTriggerExit(Collider other)
    {
        IBlackoutable blackoutable = DetectBlackoutableObject(other);

        if (blackoutable == null) return;

        blackoutable.ExitField();
    }

    private IBlackoutable DetectBlackoutableObject(Collider other)
    {
        if ((detectableLayers.value & (1 << other.gameObject.layer)) == 0)
            return null;

        if (other.TryGetComponent(out IBlackoutable blackoutable))
        {
            return blackoutable;
        }
        return null;
    }
}
