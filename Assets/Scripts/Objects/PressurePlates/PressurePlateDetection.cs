using UnityEngine;

public class PressurePlateDetector : MonoBehaviour
{
    [SerializeField] private PressurePlate plate;

    private void OnTriggerEnter(Collider other)
    {
        plate.HandleEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        plate.HandleExit(other);
    }
}
