using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private bool isPlateDown = false;

    [SerializeField] private bool isOneWayDownDetection = false;

    [Header("Model")]
    [SerializeField] private float liftedHeight = 1;
    [SerializeField] private float pressedHeight = 0.5f;
    [SerializeField] private Transform detectionPlate;

    [Header("Events")]
    [SerializeField] private SwitchEventChannelSO switchEvent;

    public void HandleEnter(Collider other)
    {
        if (other.TryGetComponent(out IWeightable weightable))
        {
            if (!isPlateDown)
            {
                isPlateDown = true;
                switchEvent.InvokeChange(true);

                Vector3 newPosition = detectionPlate.localPosition;
                newPosition.y = pressedHeight;
                detectionPlate.localPosition = newPosition;
            }
        }
    }

    public void HandleExit(Collider other)
    {
        if (other.TryGetComponent(out IWeightable weightable))
        {
            if (isPlateDown)
            {
                isPlateDown = false;

                if (!isOneWayDownDetection)
                {
                    switchEvent.InvokeChange(false);
                }

                Vector3 newPosition = detectionPlate.localPosition;
                newPosition.y = liftedHeight;
                detectionPlate.localPosition = newPosition;
            }
        }
    }
}
