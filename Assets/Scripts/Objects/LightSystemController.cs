using UnityEngine;

public class LightSystemController : MonoBehaviour
{
    [System.Serializable]
    public class LightSystemComponent
    {
        public LightSO source;
        public SwitchEventChannelSO[] switchChannels;

        public void OnSwitchTriggered()
        {
            source.SetState(!source.IsOn);
        }

        public void OnSwitchChanged(bool isOn)
        {
            source.SetState(isOn);
        }
    }

    [SerializeField] private LightSystemComponent[] lightSystems;

    private void OnEnable()
    {
        foreach (LightSystemComponent system in lightSystems)
        {
            foreach (SwitchEventChannelSO switchChannel in system.switchChannels)
            {
                switchChannel.OnToggle += system.OnSwitchTriggered;
                switchChannel.OnChange += system.OnSwitchChanged;
            }
        }
    }

    private void OnDisable()
    {
        foreach (LightSystemComponent system in lightSystems)
        {
            foreach (SwitchEventChannelSO switchChannel in system.switchChannels)
            {
                switchChannel.OnToggle -= system.OnSwitchTriggered;
                switchChannel.OnChange -= system.OnSwitchChanged;
            }
        }
    }
}
