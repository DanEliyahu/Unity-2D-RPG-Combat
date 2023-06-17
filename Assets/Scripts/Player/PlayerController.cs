using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float _moveSpeed = 5f;
    
    [Header("Dash")]
    [SerializeField] private float _dashSpeed = 20f;
    [SerializeField] private float _dashDuration = 0.2f;
    [SerializeField] private float _dashCooldown = 0.3f;
    [SerializeField] private TrailRenderer _trailRenderer;
    
    private PlayerControls _playerControls;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Camera _mainCam;
    private bool _canDash = true;
    private float _startingMoveSpeed;
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");

    protected override void Awake()
    {
        base.Awake();
        
        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startingMoveSpeed = _moveSpeed;
    }

    private void Start()
    {
        _mainCam = Camera.main;
        _playerControls.Combat.Dash.performed += Dash;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
        AdjustPlayerFacingDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayerInput()
    {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();
        _animator.SetBool(IsRunning, _movement != Vector2.zero);
    }

    private void AdjustPlayerFacingDirection()
    {
        if (!_mainCam) return;

        var mousePosition = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        _spriteRenderer.flipX = mousePosition.x < transform.position.x;
    }

    private void Move()
    {
        _rb.MovePosition(_rb.position + _movement * (_moveSpeed * Time.fixedDeltaTime));
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if (!_canDash) return;

        _canDash = false;
        _moveSpeed = _dashSpeed;
        _trailRenderer.emitting = true;
        StartCoroutine(DashCDRoutine());
    }

    private IEnumerator DashCDRoutine()
    {
        yield return new WaitForSeconds(_dashDuration);
        _moveSpeed = _startingMoveSpeed;
        _trailRenderer.emitting = false;
        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }
}