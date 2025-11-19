using UnityEngine;

public class BaseLightSwitch : MonoBehaviour, ILightSwitchable, IInterableObject
{
    [Header("Model")]
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
