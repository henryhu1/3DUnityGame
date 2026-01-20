using UnityEngine;

public class LeverSwitch : BaseSwitch
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
