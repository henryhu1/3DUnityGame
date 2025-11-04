using UnityEngine;

public class PointLight : MonoBehaviour
{
    private Light pointLight;

    [SerializeField] private LightSO lightSO;

    private void Awake()
    {
        pointLight = GetComponent<Light>();
    }

    public void Update()
    {
        pointLight.enabled = lightSO.GetIsLightOn();
    }
}
