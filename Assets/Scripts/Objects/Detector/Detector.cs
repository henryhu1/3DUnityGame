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

    [SerializeField] protected string targetTag;

    [Header("Events")]
    [SerializeField] private SwitchEventChannelSO switchEvent;

    public bool GetIsDetectingCollision()
    {
        return isDetectingCollision;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (string.IsNullOrEmpty(targetTag) || other.CompareTag(targetTag))
        {
            isDetectingCollision = true;
            DetectCollisionChange();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (string.IsNullOrEmpty(targetTag) || other.CompareTag(targetTag))
        {
            isDetectingCollision = false;
            DetectCollisionChange();
        }
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

        switchEvent.InvokeChange(isOn);
        return isOn;
    }
}
