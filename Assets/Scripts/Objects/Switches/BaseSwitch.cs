using UnityEngine;

public class BaseSwitch : MonoBehaviour, IInteractableObject
{
    [Header("Model")]
    [SerializeField] private Renderer rend;

    [SerializeField] private Color highlightColor = Color.cyan;
    private Color baseColor;

    [Header("Events")]
    [SerializeField] private SwitchEventChannelSO switchEvent;

    private void Awake()
    {
        baseColor = rend.material.GetColor("_EmissionColor");
    }

    public void SetBaseColor(Color color)
    {
        baseColor = color;
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
        switchEvent.InvokeToggle();
    }

    public void Highlight(bool isHighlighted)
    {
        if (isHighlighted)
            rend.material.SetColor("_EmissionColor", highlightColor);
        else
            rend.material.SetColor("_EmissionColor", baseColor);
    }
}
