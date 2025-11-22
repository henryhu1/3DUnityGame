using UnityEngine;

public class LightPanel : LightController
{
    [Header("Model")]
    [SerializeField] private Renderer rend;

    private BaseLightSwitch baseLightSwitch;

    private Color litColor = Color.gold;
    private Color baseColor;

    private void Awake()
    {
        baseColor = rend.material.GetColor("_EmissionColor");
    }

    private void Start()
    {
        TryGetComponent(out baseLightSwitch);
    }

    protected override void ApplyFinalState()
    {
        base.ApplyFinalState();

        bool isOn = evaluator.GetLightsState(sources);
        if (isOn)
        {
            rend.material.SetColor("_EmissionColor", litColor);
            if (baseLightSwitch != null) baseLightSwitch.SetBaseColor(litColor);
        }
        else
        {
            rend.material.SetColor("_EmissionColor", baseColor);
            if (baseLightSwitch != null) baseLightSwitch.SetBaseColor(baseColor);
        }
    }
}
