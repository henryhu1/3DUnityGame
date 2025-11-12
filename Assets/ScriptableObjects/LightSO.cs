using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LightSO", menuName = "Scriptable Objects/LightSO")]
public class LightSO : ScriptableObject, ILightable
{
    [SerializeField] private bool isLightOn;

    public event Action<bool> OnLightChangeEvent;

    public bool GetIsLightOn()
    {
        return isLightOn;
    }

    public void ToggleLight()
    {
        isLightOn = !isLightOn;
        OnLightChangeEvent?.Invoke(isLightOn);
    }

    public void TurnOff()
    {
        isLightOn = false;
        OnLightChangeEvent?.Invoke(isLightOn);
    }

    public void TurnOn()
    {
        isLightOn = true;
        OnLightChangeEvent?.Invoke(isLightOn);
    }
}
