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

    void Update()
    {
        Ray ray = new(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            GameObject hitObj = hit.collider.gameObject;
            if (hitObj.TryGetComponent(out IInterableObject interactable))
            {
                PlayerManager.Instance.SwitchHover(interactable);
            }
        }
        else
        {
            PlayerManager.Instance.LeaveHover();
        }
    }
}
