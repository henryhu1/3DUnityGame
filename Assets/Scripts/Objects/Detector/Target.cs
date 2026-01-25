using UnityEngine;

public class Target : Detector
{
    [Header("Model")]
    [SerializeField] private Renderer rend;
    [SerializeField] private Color detectedColor = Color.pink;
    [SerializeField] private Light detectionLight;

    private Color baseColor;

    private void Awake()
    {
        baseColor = rend.material.GetColor("_EmissionColor");
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        // if (string.IsNullOrEmpty(targetTag) || other.CompareTag(targetTag))
        // {
        //     rend.material.SetColor("_EmissionColor", detectedColor);
        // }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        // if (string.IsNullOrEmpty(targetTag) || other.CompareTag(targetTag))
        // {
        //     rend.material.SetColor("_EmissionColor", baseColor);
        // }
    }

    protected override bool DetectCollisionChange()
    {
        bool isOn = base.DetectCollisionChange();

        detectionLight.enabled = isOn;

        return isOn;
    }
}
