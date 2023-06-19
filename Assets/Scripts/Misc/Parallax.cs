using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float _parallaxOffset = -0.15f;

    private Vector2 _startPos;
    private Vector2 Travel => (Vector2)CameraController.Instance.MainCam.transform.position - _startPos;

    private void Start()
    {
        _startPos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = _startPos + Travel * _parallaxOffset;
    }
}
