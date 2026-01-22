using UnityEngine;

public abstract class LightGroupEvaluatorSO : ScriptableObject
{
    public abstract bool Evaluate(LightSO[] sources);
}
