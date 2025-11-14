using System.Linq;

[System.Serializable]
public class LightGroupEvaluator
{
    public bool GetLightsState(LightSO[] sources)
    {
        return sources.Any(l => l.IsOn);
    }
}
