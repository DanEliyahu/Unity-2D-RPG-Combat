using UnityEngine;

public class Sword : Weapon
{
    [SerializeField] private GameObject _slashEffectPrefab;
    [SerializeField] private Transform _slashEffectParent;
    [SerializeField] private DamageSource _weaponCollider;
    
    private Animator _animator;
    private bool _isSwingingUp;
    private GameObject _slashEffect;
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        MouseFollowWithOffset();
    }

    public override void Attack()
    {
        _animator.SetTrigger(AttackTrigger);
        _weaponCollider.gameObject.SetActive(true);
        _slashEffect = Instantiate(_slashEffectPrefab, _slashEffectParent);
        if (_isSwingingUp)
        {
            _slashEffect.transform.Rotate(180,0,0);
        }

        _isSwingingUp = !_isSwingingUp;
    }

    public void DoneAttackingAnimEvent()
    {
        _weaponCollider.gameObject.SetActive(false);
    }
}