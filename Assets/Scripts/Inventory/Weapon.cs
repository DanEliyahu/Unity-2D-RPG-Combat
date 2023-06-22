using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float _attackCooldown = 0.5f;

    public float AttackCooldown => _attackCooldown;

    public abstract void Attack();
}
