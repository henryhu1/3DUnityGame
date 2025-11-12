using UnityEngine;

public class LightSwitch : MonoBehaviour, ILightSwitchable, IHoverableObject, IHighlightableObject
{
    [Header("Model")]
    [SerializeField] private Vector3 onRotation;
    [SerializeField] private Vector3 offRotation;
    [SerializeField] private Transform pointOfRotation;
    [SerializeField] private Renderer rend;

    [SerializeField] private Color highlightColor = Color.cyan;
    private Color baseColor;

    [Header("Lights")]
    [SerializeField] private LightSO[] controlledLights;

    private void Start()
    {
        baseColor = rend.material.GetColor("_EmissionColor");
    }

    private void OnEnable()
    {
        foreach (LightSO light in controlledLights)
        {
            light.OnLightChangeEvent += SetLeverPosition;
        }
    }

    private void OnDisable()
    {
        foreach (LightSO light in controlledLights)
        {
            light.OnLightChangeEvent -= SetLeverPosition;
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
        Highlight(true);
    }

    public void OnCenterExit()
    {
        Highlight(false);
    }

    public void OnCenterClick()
    {
        ToggleLight();
    }

    public void Highlight(bool isOn)
    {
        if (isOn)
            rend.material.SetColor("_EmissionColor", highlightColor);
        else
            rend.material.SetColor("_EmissionColor", baseColor);
    }
}
