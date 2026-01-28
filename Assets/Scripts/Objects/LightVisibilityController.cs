using System;
using UnityEngine;

public class LightVisibilityController : MonoBehaviour, IBlackoutable
{
    [SerializeField] private Light[] controlledLights;
    [SerializeField] protected LightSO[] sources;
    [SerializeField] private LightGroupEvaluatorSO evaluator;

    public Action<bool> OnLightVisibilityChange;

    private void OnEnable()
    {
        foreach (var so in sources)
            so.OnLightChanged += OnSourceChanged;

        ApplyFinalState();
    }

    private void OnDisable()
    {
        foreach (var so in sources)
            so.OnLightChanged -= OnSourceChanged;
    }

    private void OnSourceChanged(bool _)
    {
        ApplyFinalState();
    }

    protected virtual void ApplyFinalState()
    {
        bool isOn = GetAreLightsOn();
        OnLightVisibilityChange?.Invoke(isOn);
        foreach (Light light in controlledLights)
            light.enabled = isOn;
    }

    public void EnterField()
    {
        foreach (Light light in controlledLights)
            light.enabled = false;
    }

    public void ExitField()
    {
        foreach (Light light in controlledLights)
            light.enabled = true;
    }

    public bool GetAreLightsOn()
    {
        return evaluator.Evaluate(sources);
    }
}
