using UnityEngine;

[CreateAssetMenu(fileName = "LightRegistrySO", menuName = "Scriptable Objects/LightRegistrySO")]
public class LightRegistrySO : ScriptableObject
{
    public LightSO[] lights;
}
