using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    
    [Header("Dash")]
    [SerializeField] private float _dashSpeedMultiplier = 3f;
    [SerializeField] private float _dashDuration = 0.2f;
    [SerializeField] private float _dashCooldown = 0.3f;
    [SerializeField] private TrailRenderer _trailRenderer;

    public static PlayerController Instance;
    private PlayerControls _playerControls;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Camera _mainCam;
    private bool _canDash = true;
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");

    private void Awake()
    {
        Instance = this;
        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
        _moveSpeed *= _dashSpeedMultiplier;
        _trailRenderer.emitting = true;
        StartCoroutine(DashCDRoutine());
    }

    private IEnumerator DashCDRoutine()
    {
        yield return new WaitForSeconds(_dashDuration);
        _moveSpeed /= _dashSpeedMultiplier;
        _trailRenderer.emitting = false;
        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }
}