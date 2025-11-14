using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightController : MonoBehaviour
{
    [SerializeField] private LightSO[] sources;
    [SerializeField] private LightGroupEvaluator evaluator = new();

    private Light unityLight;

    private void Awake()
    {
        unityLight = GetComponent<Light>();
    }

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
        unityLight.enabled = isOn;
    }
}
