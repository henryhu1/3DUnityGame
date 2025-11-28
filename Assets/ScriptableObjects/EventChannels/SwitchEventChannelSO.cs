using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Switch Event Channel")]
public class SwitchEventChannelSO : ScriptableObject
{
    public event Action OnToggle;
    public event Action<bool> OnChange;

    public void InvokeToggle()
    {
        OnToggle?.Invoke();
    }

    public void InvokeChange(bool isOn)
    {
        OnChange?.Invoke(isOn);
    }
}
