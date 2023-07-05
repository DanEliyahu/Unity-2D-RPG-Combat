using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private Transform _arrowSpawnPoint;

    private Animator _animator;
    private static readonly int Fire = Animator.StringToHash("Fire");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public override void Attack()
    {
        _animator.SetTrigger(Fire);
        Instantiate(_arrowPrefab, _arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
    }

    private void Update()
    {
        FaceMouse();
    }
}
