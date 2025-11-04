using UnityEngine;

public class LightSwitch : MonoBehaviour, ILightSwitchable, IHoverableObject
{
    [Header("Model")]
    [SerializeField] private Vector3 onRotation;
    [SerializeField] private Vector3 offRotation;
    [SerializeField] private Transform pointOfRotation;

    [Header("Lights")]
    [SerializeField] private LightSO[] controlledLights;

    private bool areLightsOn;

    public void Start()
    {
        areLightsOn = true;
    }

    public void OnEnable()
    {
        ToggleLight(areLightsOn);
    }

    public void ToggleLight(bool isOn)
    {
        if (areLightsOn == isOn) return;

        if (isOn)
        {
            pointOfRotation.rotation = Quaternion.Euler(onRotation);
        }
        else
        {
            pointOfRotation.rotation = Quaternion.Euler(offRotation);
        }

        foreach (ILightable light in controlledLights)
        {
            if (isOn)
            {
                light.TurnOn();
            }
            else
            {
                light.TurnOff();
            }
        }

        areLightsOn = isOn;
    }

    public void OnCenterEnter()
    {
        Debug.Log("Center enter light switch");
    }

    public void OnCenterExit()
    {
        Debug.Log("Center exit light switch");
    }

    public void OnCenterClick()
    {
        ToggleLight(!areLightsOn);
    }
}
