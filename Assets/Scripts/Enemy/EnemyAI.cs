using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _roamChangeDirTime = 2f;
    
    private enum State
    {
        Roaming
    }

    private State _state;
    private EnemyPathfinding _enemyPathfinding;

    private void Awake()
    {
        _state = State.Roaming;
        _enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine()
    {
        while (_state == State.Roaming)
        {
            _enemyPathfinding.SetMovementDirection(GetRoamingDirection());
            yield return new WaitForSeconds(_roamChangeDirTime);
        }
    }

    private Vector2 GetRoamingDirection()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
