using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private Rigidbody2D _rb;
    private Knockback _knockBack;
    private SpriteRenderer _spriteRenderer;
    
    private Vector2 _movementDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _knockBack = GetComponent<Knockback>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (_knockBack.IsKnockedBack) return;
        
        _rb.MovePosition(_rb.position + _movementDirection * (_moveSpeed * Time.fixedDeltaTime));

        _spriteRenderer.flipX = _movementDirection.x < 0;
    }

    public void SetMovementDirection(Vector2 direction)
    {
        _movementDirection = direction;
    }

    public void StopMoving()
    {
        _movementDirection = Vector2.zero;
    }
}
