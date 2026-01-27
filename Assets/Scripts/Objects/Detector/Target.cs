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

    protected override bool DetectCollisionChange()
    {
        bool isOn = base.DetectCollisionChange();

        detectionLight.enabled = isOn;

        return isOn;
    }
}
