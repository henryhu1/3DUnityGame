using Unity.Cinemachine;
using UnityEngine;

public class CameraDirector : MonoBehaviour
{
    [SerializeField] private Transform playerHead;
    [SerializeField] private CameraEventChannel channel;
    [SerializeField] private CinemachineCamera playerCam;
    [SerializeField] private CinemachineCamera focusCam;
    [SerializeField] private CinemachineCamera cinematicCam;

    private void Start()
    {
        playerCam.Follow = playerHead;
        playerCam.LookAt = playerHead;
    }

    private void OnEnable()
    {
        channel.OnFocusTargetRequested += FocusCamera;
        channel.OnReturnControlRequested += ReturnToPlayer;
    }

    private void OnDisable()
    {
        channel.OnFocusTargetRequested -= FocusCamera;
        channel.OnReturnControlRequested -= ReturnToPlayer;
    }

    private void FocusCamera(Transform target)
    {
        focusCam.Follow = target;
        focusCam.LookAt = target;
        focusCam.Priority = 20;
    }

    private void ReturnToPlayer()
    {
        playerCam.Priority = 20;
        focusCam.Priority = 10;
    }
}
