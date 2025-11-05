using UnityEngine;

[RequireComponent(typeof(Light))]
public class PointLight : MonoBehaviour
{
    private Light pointLight;

    [SerializeField] private LightSO lightSO;

    private void Awake()
    {
        pointLight = GetComponent<Light>();
    }

    private void OnEnable()
    {
        lightSO.RegisterOnLightChangeCallback(ToggleLight);
    }

    private void OnDisable()
    {
        lightSO.UnregisterOnLightChangeCallback(ToggleLight);
    }

    public void ToggleLight(bool isLightOn)
    {
        pointLight.enabled = isLightOn;
    }
}
