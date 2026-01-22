using UnityEngine;

public class LightVisibilityController : MonoBehaviour
{
    [SerializeField] private Light[] controlledLights;
    [SerializeField] protected LightSO[] sources;
    [SerializeField] private LightGroupEvaluatorSO evaluator;

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
        bool isOn = evaluator.Evaluate(sources);
        foreach (Light light in controlledLights)
            light.enabled = isOn;
    }
}
