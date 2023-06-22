using UnityEngine;

public class Sword : Weapon
{
    [SerializeField] private GameObject _slashEffectPrefab;
    [SerializeField] private Transform _slashEffectParent;
    [SerializeField] private DamageSource _weaponCollider;
    
    private Animator _animator;
    private PlayerController _playerController;
    private ActiveWeapon _activeWeapon;
    private bool _isSwingingUp;
    private GameObject _slashEffect;
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponentInParent<PlayerController>();
        _activeWeapon = GetComponentInParent<ActiveWeapon>();
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


    private void MouseFollowWithOffset()
    {
        var mousePos = Input.mousePosition;
        var mouseWorldPosition = CameraController.Instance.MainCam.ScreenToWorldPoint(mousePos);
        
        var zRotation = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        var yRotation = mouseWorldPosition.x < _playerController.transform.position.x ? 180 : 0;
        
        _activeWeapon.transform.rotation = Quaternion.Euler(0, yRotation, zRotation);
    }
}