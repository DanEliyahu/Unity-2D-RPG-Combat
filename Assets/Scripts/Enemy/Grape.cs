using UnityEngine;

public class Grape : BaseEnemy
{
    [SerializeField] private GameObject _grapeProjectilePrefab;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Attack()
    {
        _animator.SetTrigger(AttackTrigger);
        _spriteRenderer.flipX = transform.position.x >  PlayerController.Instance.transform.position.x;
    }

    public void SpawnProjectileAnimEvent()
    {
        Instantiate(_grapeProjectilePrefab, transform.position, Quaternion.identity);
    }
}
