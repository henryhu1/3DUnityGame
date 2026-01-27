using UnityEngine;

public class WallLamp : MonoBehaviour
{
    [Header("Logic")]
    [SerializeField] private LightVisibilityController visibility;

    [Header("Model")]
    [SerializeField] private Renderer lampRenderer;

    private void OnEnable()
    {
        visibility.OnLightVisibilityChange += DisplayMaterial;
    }

    private void OnDisable()
    {
        visibility.OnLightVisibilityChange -= DisplayMaterial;
    }

    private void DisplayMaterial(bool isVisible)
    {
        if (isVisible) lampRenderer.material.SetColor("_EmissionColor", Color.white * 1);
        else lampRenderer.material.SetColor("_EmissionColor", Color.white * 0);
    }
}
