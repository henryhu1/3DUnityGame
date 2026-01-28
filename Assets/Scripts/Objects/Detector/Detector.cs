using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Detector : MonoBehaviour
{
    protected enum DetectorType
    {
        ENTER_ON,
        ENTER_OFF,
        EXIT_ON,
        EXIT_OFF,
    }

    protected bool isDetectingCollision;

    [SerializeField] protected DetectorType detectorType = DetectorType.ENTER_OFF;

    [SerializeField] protected float checkRadius = 1f;
    [SerializeField] protected string targetTag;

    [Header("Events")]
    [SerializeField] private SwitchEventChannelSO switchEvent;

    public bool GetIsDetectingCollision()
    {
        return isDetectingCollision;
    }

    private void Update()
    {
        if (!isDetectingCollision && detectorType != DetectorType.EXIT_OFF)
        {
            CheckForDetection();
            bool isOn = DetectCollisionChange();
            // TODO: only invoke event if it is different
            switchEvent.InvokeChange(isOn);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (string.IsNullOrEmpty(targetTag) || other.CompareTag(targetTag))
        {
            isDetectingCollision = true;
            bool isOn = DetectCollisionChange();
            switchEvent.InvokeChange(isOn);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (string.IsNullOrEmpty(targetTag) || other.CompareTag(targetTag))
        {
            isDetectingCollision = false;
            bool isOn = DetectCollisionChange();
            switchEvent.InvokeChange(isOn);
        }
    }

    private void CheckForDetection()
    {
        Collider[] hitColliders = {};
        Physics.OverlapSphereNonAlloc(transform.position, checkRadius, hitColliders, LayerMask.GetMask("Default"));

        if (hitColliders.Length > 0)
        {
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag(targetTag))
                {
                    isDetectingCollision = true;
                    break;
                }
            }
        }
        isDetectingCollision = false;
    }

    protected virtual bool DetectCollisionChange()
    {
        bool isOn;
        if (isDetectingCollision)
        {
            if (detectorType == DetectorType.ENTER_ON) isOn = true;
            else if (detectorType == DetectorType.ENTER_OFF) isOn = false;
            else isOn = false;
        }
        else
        {
            if (detectorType == DetectorType.EXIT_ON) isOn = true;
            else if (detectorType == DetectorType.EXIT_OFF) isOn = false;
            else isOn = false;
        }
        return isOn;
    }
}
