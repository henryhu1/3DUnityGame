using UnityEngine;

public class PressurePlate : MonoBehaviour, ILightSwitchable
{
    private bool isPlateDown = false;

    [Header("Model")]
    [SerializeField] private float liftedHeight = 1;
    [SerializeField] private float pressedHeight = 0.5f;
    [SerializeField] private Transform detectionPlate;

    [Header("Lights")]
    [SerializeField] private LightController[] controlledLights;

    public void HandleEnter(Collider other)
    {
        if (other.TryGetComponent(out IWeightable weightable))
        {
            if (!isPlateDown)
            {
                isPlateDown = true;
                ToggleLight();

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
                ToggleLight();

                Vector3 newPosition = detectionPlate.localPosition;
                newPosition.y = liftedHeight;
                detectionPlate.localPosition = newPosition;
            }
        }
    }

    public void ToggleLight()
    {
        foreach (LightController light in controlledLights)
        {
            if (isPlateDown) light.TurnOff();
            else light.TurnOn();
        }
    }
}
