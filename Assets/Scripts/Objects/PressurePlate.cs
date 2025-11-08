using UnityEngine;

public class PressurePlate : MonoBehaviour, ILightSwitchable
{
    private bool isPlateDown = false;

    [Header("Lights")]
    [SerializeField] private LightSO[] controlledLights;

    public void HandleEnter(Collider other)
    {
        if (!isPlateDown)
        {
            ToggleLight();
            isPlateDown = true;
        }
    }

    public void HandleExit(Collider other)
    {
        if (isPlateDown)
        {
            ToggleLight();
            isPlateDown = false;
        }
    }

    public void ToggleLight()
    {
        foreach (LightSO light in controlledLights)
        {
            light.ToggleLight();
        }
    }
}
