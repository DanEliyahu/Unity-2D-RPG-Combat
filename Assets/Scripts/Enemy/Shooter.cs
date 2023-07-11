using UnityEngine;

public class Shooter : BaseEnemy
{
    [SerializeField] private GameObject _bulletPrefab;

    public override void Attack()
    {
        var myPosition = transform.position;
        Vector2 targetDirection = PlayerController.Instance.transform.position - myPosition;

        GameObject newBullet = Instantiate(_bulletPrefab, myPosition, Quaternion.identity);
        newBullet.transform.right = targetDirection;
    }
}
