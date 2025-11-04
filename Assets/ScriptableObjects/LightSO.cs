using UnityEngine;

[CreateAssetMenu(fileName = "LightSO", menuName = "Scriptable Objects/LightSO")]
public class LightSO : ScriptableObject, ILightable
{
    [SerializeField] private bool isLightOn;

    public bool GetIsLightOn()
    {
        return isLightOn;
    }

    public void TurnOff()
    {
        isLightOn = false;
    }

    public void TurnOn()
    {
        isLightOn = true;
    }
}
