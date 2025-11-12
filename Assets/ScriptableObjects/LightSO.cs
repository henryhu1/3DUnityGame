using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LightSO", menuName = "Scriptable Objects/LightSO")]
public class LightSO : ScriptableObject
{
    [SerializeField] private bool isOn;

    /// <summary>
    /// Fired when the light data changes (true = ON, false = OFF).
    /// </summary>
    public event Action<bool> OnLightChanged;

    public bool IsOn => isOn;

    /// <summary>
    /// Set the light value. Will only invoke event if changed.
    /// </summary>
    public void SetState(bool newState)
    {
        if (isOn == newState)
            return;

        isOn = newState;
        OnLightChanged?.Invoke(isOn);
    }
}