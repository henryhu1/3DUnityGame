using UnityEngine;

public class LightPowerController : MonoBehaviour, ISwitchable
{
    [Header("Events")]
    [SerializeField] private SwitchEventChannelSO switchChannel;

    [Header("Lights")]
    [SerializeField] private LightSO[] sources;

    private void OnEnable()
    {
        if (switchChannel != null)
        {
            switchChannel.OnToggle += OnSwitchTriggered;
            switchChannel.OnChange += OnSwitchChanged;
        }
    }

    private void OnDisable()
    {
        if (switchChannel != null)
        {
            switchChannel.OnToggle -= OnSwitchTriggered;
            switchChannel.OnChange -= OnSwitchChanged;
        }
    }

    public void OnSwitchTriggered()
    {
        foreach (var so in sources)
            so.SetState(!so.IsOn);
    }

    public void OnSwitchChanged(bool isOn)
    {
        foreach (var so in sources)
            so.SetState(isOn);
    }
}
