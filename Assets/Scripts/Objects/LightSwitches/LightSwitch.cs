using UnityEngine;

public class LightSwitch : BaseLightSwitch
{
    [Header("Lever")]
    [SerializeField] private Vector3 onRotation;
    [SerializeField] private Vector3 offRotation;
    [SerializeField] private Transform pointOfRotation;

    private void SetLeverPosition(bool isLightOn)
    {
        if (isLightOn)
        {
            pointOfRotation.rotation = Quaternion.Euler(onRotation);
        }
        else
        {
            pointOfRotation.rotation = Quaternion.Euler(offRotation);
        }
    }
}
