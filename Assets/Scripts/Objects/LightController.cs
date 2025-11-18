using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private Light[] controlledLights;
    [SerializeField] private LightSO[] sources;
    private LightGroupEvaluator evaluator = new();

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

    private void ApplyFinalState()
    {
        bool isOn = evaluator.GetLightsState(sources);
        foreach (Light light in controlledLights)
            light.enabled = isOn;
    }
}
