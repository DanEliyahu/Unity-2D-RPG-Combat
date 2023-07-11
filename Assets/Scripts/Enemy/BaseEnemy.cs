using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected float _attackCooldown = 1f;

    public bool CanAttack { get; protected set; } = true;

    public abstract void Attack();
}
