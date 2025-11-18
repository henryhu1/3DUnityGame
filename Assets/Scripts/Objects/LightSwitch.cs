using UnityEngine;

public class LightSwitch : MonoBehaviour, ILightSwitchable, IInterableObject
{
    [Header("Model")]
    [SerializeField] private Vector3 onRotation;
    [SerializeField] private Vector3 offRotation;
    [SerializeField] private Transform pointOfRotation;
    [SerializeField] private Renderer rend;

    [SerializeField] private Color highlightColor = Color.cyan;
    private Color baseColor;

    [Header("Lights")]
    [SerializeField] private LightSO[] sources;

    private void Start()
    {
        baseColor = rend.material.GetColor("_EmissionColor");
    }

    public void ToggleLight()
    {
        foreach (LightSO light in sources)
        {
            light.SetState(!light.IsOn);
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

    public void OnInteractHover()
    {
        Highlight(true);
    }

    public void OnInteractExit()
    {
        Highlight(false);
    }

    public void OnInteract()
    {
        ToggleLight();
    }

    public void Highlight(bool isHighlighted)
    {
        if (isHighlighted)
            rend.material.SetColor("_EmissionColor", highlightColor);
        else
            rend.material.SetColor("_EmissionColor", baseColor);
    }
}
