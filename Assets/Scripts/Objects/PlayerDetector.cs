using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerDetector : MonoBehaviour, ILightSwitchable
{
    private Collider detector;
    
    [Header("Lights")]
    [SerializeField] private LightSO[] sources;

    private void Awake()
    {
        detector = GetComponent<Collider>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleLight();
        }
    }

    public void ToggleLight()
    {
        foreach (LightSO light in sources)
        {
            light.SetState(false);
        }
    }
}
