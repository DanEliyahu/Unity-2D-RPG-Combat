using UnityEngine;

public class Staff : Weapon
{
    [SerializeField] private GameObject _magicLaser;
    [SerializeField] private Transform _magicLaserSpawnPoint;

    private Animator _animator;
    private static readonly int Fire = Animator.StringToHash("Fire");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public override void Attack()
    {
        _animator.SetTrigger(Fire);
    }

    public void SpawnStaffProjectileAnimEvent()
    {
        Instantiate(_magicLaser, _magicLaserSpawnPoint.position, Quaternion.identity);
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }
}
