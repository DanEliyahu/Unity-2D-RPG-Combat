using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject _slashEffectPrefab;
    [SerializeField] private Transform _slashEffectParent;
    [SerializeField] private DamageSource _weaponCollider;
    
    private PlayerControls _playerControls;
    private Animator _animator;
    private PlayerController _playerController;
    private ActiveWeapon _activeWeapon;
    private Camera _mainCam;
    private bool _isSwingingUp;
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
        _playerControls.Combat.Attack.started += Attack;
        _mainCam = Camera.main;
    }

    private void Attack(InputAction.CallbackContext context)
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

    private void Update()
    {
        MouseFollowWithOffset();
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