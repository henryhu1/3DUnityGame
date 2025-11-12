using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightController : MonoBehaviour, ILightable
{
    [SerializeField] private LightSO lightData;

    private Light unityLight;

    private void Awake()
    {
        unityLight = GetComponent<Light>();
    }

    private void OnEnable()
    {
        if (lightData != null)
        {
            lightData.OnLightChanged += Apply;
            Apply(lightData.IsOn);
        }
    }

    private void OnDisable()
    {
        if (lightData != null)
            lightData.OnLightChanged -= Apply;
    }

    private void Apply(bool isOn)
    {
        unityLight.enabled = isOn;
    }

    public void TurnOn()
    {
        lightData.SetState(true);
    }

    public void TurnOff()
    {
        lightData.SetState(false);
    }

    public void ToggleLight()
    {
        lightData.SetState(!lightData.IsOn);
    }
}
