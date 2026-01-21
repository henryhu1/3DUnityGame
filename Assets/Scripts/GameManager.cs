using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LightRegistrySO registry;

    public float maxDistance = 100f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        foreach (var light in registry.lights)
        {
            light.SetState(true);
        }
    }
}
