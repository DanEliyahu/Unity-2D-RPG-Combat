using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 22f;
    [SerializeField] private float _projectileRange = 10f;

    private Rigidbody2D _rb;
    private Vector3 _startPosition;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        var myTransform = transform;
        _startPosition = myTransform.position;
        _rb.velocity = myTransform.right * _moveSpeed;
    }

    private void Update()
    {
        DetectFireDistance();
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(_startPosition, transform.position) > _projectileRange)
        {
            Destroy(gameObject);
        }
    }
}
