using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    [SerializeField] private float _attackCooldown = 0.5f;
    [SerializeField] private GameObject _slashEffectPrefab;
    [SerializeField] private Transform _slashEffectParent;
    [SerializeField] private DamageSource _weaponCollider;
    
    private PlayerControls _playerControls;
    private Animator _animator;
    private PlayerController _playerController;
    private ActiveWeapon _activeWeapon;
    private Camera _mainCam;
    private bool _isSwingingUp, _attackButtonDown, _isAttacking;
    private GameObject _slashEffect;
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _animator = GetComponent<Animator>();
        _playerController = GetComponentInParent<PlayerController>();
        _activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Start()
    {
        _playerControls.Combat.Attack.started += StartAttacking;
        _playerControls.Combat.Attack.canceled += StopAttacking;
        _mainCam = Camera.main;
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
        MouseFollowWithOffset();
        Attack();
    }

    private void Attack()
    {
        if (!_attackButtonDown || _isAttacking) return;
        
        _isAttacking = true;
        _animator.SetTrigger(AttackTrigger);
        _weaponCollider.gameObject.SetActive(true);
        _slashEffect = Instantiate(_slashEffectPrefab, _slashEffectParent);
        if (_isSwingingUp)
        {
            _slashEffect.transform.Rotate(180,0,0);
        }

        _isSwingingUp = !_isSwingingUp;
        StartCoroutine(AttackCDRoutine());
    }

    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(_attackCooldown);
        _isAttacking = false;
    }

    public void DoneAttackingAnimEvent()
    {
        _weaponCollider.gameObject.SetActive(false);
    }


    private void MouseFollowWithOffset()
    {
        if (!_mainCam) return;

        var mousePos = Input.mousePosition;
        var mouseWorldPosition = _mainCam.ScreenToWorldPoint(mousePos);
        
        var zRotation = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        var yRotation = mouseWorldPosition.x < _playerController.transform.position.x ? 180 : 0;
        
        _activeWeapon.transform.rotation = Quaternion.Euler(0, yRotation, zRotation);
    }
}