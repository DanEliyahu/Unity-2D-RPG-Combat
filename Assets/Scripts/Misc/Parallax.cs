using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float _parallaxOffset = -0.15f;

    private Camera _cam;
    private Vector2 _startPos;
    private Vector2 Travel => (Vector2)_cam.transform.position - _startPos;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Start()
    {
        _startPos = transform.position;
    }

    private void LateUpdate()
    {
        transform.position = _startPos + Travel * _parallaxOffset;
    }
}
