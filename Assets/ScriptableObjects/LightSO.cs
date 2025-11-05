using UnityEngine;

[CreateAssetMenu(fileName = "LightSO", menuName = "Scriptable Objects/LightSO")]
public class LightSO : ScriptableObject, ILightable
{
    [SerializeField] private bool isLightOn;

    public delegate void OnLightChange(bool isLightOn);
    private event OnLightChange OnLightChangeEvent;

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

    public void RegisterOnLightChangeCallback(OnLightChange callback)
    {
        OnLightChangeEvent += callback;
    }

    public void UnregisterOnLightChangeCallback(OnLightChange callback)
    {
        OnLightChangeEvent -= callback;
    }
}
