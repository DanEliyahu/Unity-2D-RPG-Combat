using Cinemachine;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public Camera MainCam
    {
        get
        {
            if (!_mainCam)
            {
                _mainCam = Camera.main;
            }
            return _mainCam;
        }
    }
    private CinemachineVirtualCamera _virtualCamera;
    private Camera _mainCam;

    public void SetPlayerCameraFollow()
    {
        _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _virtualCamera.Follow = PlayerController.Instance.transform;
    }
}
