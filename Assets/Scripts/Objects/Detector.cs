using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Detector : MonoBehaviour, ILightSwitchable
{
    private Collider detector;

    [SerializeField] private string targetTag;
    
    [Header("Lights")]
    [SerializeField] private LightSO[] sources;

    private void Awake()
    {
        detector = GetComponent<Collider>();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"trigger exit {name}");
        if (string.IsNullOrEmpty(targetTag) || other.CompareTag(targetTag))
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
