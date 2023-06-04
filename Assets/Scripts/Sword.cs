using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    private PlayerControls _playerControls;
    private Animator _animator;
    private PlayerController _playerController;
    private ActiveWeapon _activeWeapon;
    private Camera _mainCam;
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