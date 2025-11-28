using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Camera Event Channel")]
public class CameraEventChannel : ScriptableObject
{
    public event Action<Transform> OnFocusTargetRequested;
    public event Action OnReturnControlRequested;

    public void RaiseFocus(Transform target)
        => OnFocusTargetRequested?.Invoke(target);

    public void RaiseReturn()
        => OnReturnControlRequested?.Invoke();
}
