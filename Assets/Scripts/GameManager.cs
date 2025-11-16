using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float maxDistance = 100f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
