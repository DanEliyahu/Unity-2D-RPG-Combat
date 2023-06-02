using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    private PlayerControls _playerControls;
    private Animator _animator;
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Start()
    {
        _playerControls.Combat.Attack.started += Attack;
    }

    private void Attack(InputAction.CallbackContext context)
    {
        _animator.SetTrigger(AttackTrigger);
    }
}
