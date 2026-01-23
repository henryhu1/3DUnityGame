using UnityEngine;

public class LeverSwitch : BaseSwitch
{
    [Header("Lever")]
    [SerializeField] private Vector3 onRotation;
    [SerializeField] private Vector3 offRotation;
    [SerializeField] private Transform pointOfRotation;

    private bool isOnRotation;

    public void OnEnable()
    {
        isOnRotation = true;
        SetLeverPosition();
    }

    public override void OnInteract(InteractionContext _)
    {
        base.OnInteract(_);

        ToggleLeverRotation();
        SetLeverPosition();
    }

    private void ToggleLeverRotation()
    {
        isOnRotation = !isOnRotation;
    }

    private void SetLeverPosition()
    {
        if (isOnRotation)
        {
            pointOfRotation.localRotation = Quaternion.Euler(onRotation);
        }
        else
        {
            pointOfRotation.localRotation = Quaternion.Euler(offRotation);
        }
    }
}
