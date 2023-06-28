using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    private Weapon _currentWeapon;
    private PlayerControls _playerControls;
    private bool _attackButtonDown, _isAttacking;

    protected override void Awake()
    {
        base.Awake();
        
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }
    
    private void Start()
    {
        _playerControls.Combat.Attack.started += StartAttacking;
        _playerControls.Combat.Attack.canceled += StopAttacking;
    }

    private void StartAttacking(InputAction.CallbackContext context)
    {
        _attackButtonDown = true;
    }

    private void StopAttacking(InputAction.CallbackContext context)
    {
        _attackButtonDown = false;
    }

    private void Update()
    {
        Attack();
    }

    public void SetWeapon(Weapon newWeapon)
    {
        if (_currentWeapon != null)
        {
            Destroy(_currentWeapon.gameObject);
        }

        _currentWeapon = newWeapon;
    }
    
    private void Attack()
    {
        if (!_attackButtonDown || _isAttacking) return;
        
        _isAttacking = true;
        _currentWeapon.Attack();
        StartCoroutine(AttackCDRoutine());
    }
    
    
    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(_currentWeapon.AttackCooldown);
        _isAttacking = false;
    }
}
