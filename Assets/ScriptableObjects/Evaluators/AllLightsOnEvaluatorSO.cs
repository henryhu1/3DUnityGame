using UnityEngine;

[CreateAssetMenu(menuName = "Evaluators/All On")]
public class AllLightsOnEvaluatorSO : LightGroupEvaluatorSO
{
    public override bool Evaluate(LightSO[] sources)
    {
        foreach (var so in sources)
            if (!so.IsOn)
                return false;

        return true;
    }
}
