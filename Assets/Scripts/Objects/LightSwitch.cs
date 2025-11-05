using UnityEngine;

public class LightSwitch : MonoBehaviour, ILightSwitchable, IHoverableObject
{
    [Header("Model")]
    [SerializeField] private Vector3 onRotation;
    [SerializeField] private Vector3 offRotation;
    [SerializeField] private Transform pointOfRotation;

    [Header("Lights")]
    [SerializeField] private LightSO[] controlledLights;

    private void OnEnable()
    {
        foreach (LightSO light in controlledLights)
        {
            light.RegisterOnLightChangeCallback(SetLeverPosition);
        }
    }

    private void OnDisable()
    {
        foreach (LightSO light in controlledLights)
        {
            light.UnregisterOnLightChangeCallback(SetLeverPosition);
        }
    }

    public void ToggleLight()
    {
        foreach (LightSO light in controlledLights)
        {
            light.ToggleLight();
        }
    }

    private void SetLeverPosition(bool isLightOn)
    {
        if (isLightOn)
        {
            pointOfRotation.rotation = Quaternion.Euler(onRotation);
        }
        else
        {
            pointOfRotation.rotation = Quaternion.Euler(offRotation);
        }
    }

    public void OnCenterEnter()
    {
        // @TODO: visual effect on hover
    }

    public void OnCenterExit()
    {
        // @TODO: visual effect on hover
    }

    public void OnCenterClick()
    {
        ToggleLight();
    }
}
