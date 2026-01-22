using UnityEngine;

[CreateAssetMenu(menuName = "Evaluators/Any On")]
public class AnyLightOnEvaluatorSO : LightGroupEvaluatorSO
{
    public override bool Evaluate(LightSO[] sources)
    {
        foreach (var so in sources)
            if (so.IsOn)
                return true;

        return false;
    }
}
