using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _roamChangeDirTime = 2f;
    [SerializeField] private float _attackRange = 5f;
    [SerializeField] private BaseEnemy _enemyType;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private bool _stopMovingWhileAttacking;

    private EnemyPathfinding _enemyPathfinding;
    
    private enum State
    {
        Roaming,
        Attacking
    }

    private State _state;
    private Vector2 _roamDirection;
    private float _timeRoaming;
    private bool _canAttack = true;

    private void Awake()
    {
        _state = State.Roaming;
        _enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    private void Start()
    {
        _roamDirection = GetRoamingDirection();
        _enemyPathfinding.SetMovementDirection(_roamDirection);
    }

    private void Update()
    {
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch (_state)
        {
            case State.Roaming:
                Roam();
                break;
            case State.Attacking:
                Attack();
                break;
        }
    }

    private void Roam()
    {
        _timeRoaming += Time.deltaTime;

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < _attackRange)
        {
            _state = State.Attacking;
            return;
        }

        if (_timeRoaming > _roamChangeDirTime)
        {
            _enemyPathfinding.SetMovementDirection(_roamDirection);
            _timeRoaming = 0;
            _roamDirection = GetRoamingDirection();
        }
    }

    private void Attack()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > _attackRange)
        {
            _state = State.Roaming;
            return;
        }
        
        if (!_canAttack) return;
        
        _enemyType.Attack();
        if (_stopMovingWhileAttacking)
        {
            _enemyPathfinding.StopMoving();
        }
        _canAttack = false;
        StartCoroutine(AttackCooldownRoutine());
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }

    private Vector2 GetRoamingDirection()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
