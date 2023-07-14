using System.Collections;
using UnityEngine;

public class Shooter : BaseEnemy
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _burstCount;
    [SerializeField] private int _bulletsPerBurst;
    [SerializeField] [Range(0, 359)] private float _angleSpread;
    [SerializeField] private float _startingDistance = 0.1f;
    [SerializeField] private float _timeBetweenShotsInBurst;

    private void OnValidate()
    {
        if (_angleSpread == 0)
        {
            _bulletsPerBurst = 1;
        }
    }

    public override void Attack()
    {
        CanAttack = false;
        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        for (int i = 0; i < _burstCount; i++)
        {
            var myPosition = transform.position;
            Vector2 targetDirection = PlayerController.Instance.transform.position - myPosition;
            float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            float currentAngle = targetAngle;
            float angleStep = 0f;
            if (_bulletsPerBurst > 1)
            {
                angleStep = _angleSpread / (_bulletsPerBurst - 1);
                float startAngle = targetAngle - (_angleSpread * 0.5f);
                currentAngle = startAngle;
            }

            for (int j = 0; j < _bulletsPerBurst; j++)
            {
                GameObject newBullet = Instantiate(_bulletPrefab, GetBulletSpawnPoint(myPosition, currentAngle),
                    Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - myPosition;
                currentAngle += angleStep;
            }
            
            yield return new WaitForSeconds(_timeBetweenShotsInBurst);
        }

        yield return new WaitForSeconds(_attackCooldown);
        CanAttack = true;
    }

    private Vector2 GetBulletSpawnPoint(Vector2 position, float currentAngle)
    {
        float x = position.x + _startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = position.y + _startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        return new Vector2(x, y);
    }
}
