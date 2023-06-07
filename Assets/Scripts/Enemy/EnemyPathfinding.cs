using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private Rigidbody2D _rb;
    private Knockback _knockBack;
    private Vector2 _movementDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _knockBack = GetComponent<Knockback>();
    }

    private void FixedUpdate()
    {
        if (_knockBack.IsKnockedBack) return;
        
        _rb.MovePosition(_rb.position + _movementDirection * (_moveSpeed * Time.fixedDeltaTime));
    }

    public void SetMovementDirection(Vector2 direction)
    {
        _movementDirection = direction;
    }
}
