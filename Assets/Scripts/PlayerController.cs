using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private PlayerControls _playerControls;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Camera _mainCam;
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _mainCam = Camera.main;
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
}
