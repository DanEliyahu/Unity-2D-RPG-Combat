using Cinemachine;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public Camera MainCam { get; private set; }
    private CinemachineVirtualCamera _virtualCamera;

    protected override void Awake()
    {
        base.Awake();
        
        MainCam = Camera.main;
    }

    public void SetPlayerCameraFollow()
    {
        _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _virtualCamera.Follow = PlayerController.Instance.transform;
        MainCam = Camera.main;
    }
}
