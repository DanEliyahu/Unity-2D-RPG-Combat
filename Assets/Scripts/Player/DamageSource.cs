using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private bool _isEnemySource;
    [SerializeField] private int _damageAmount = 1;
    [SerializeField] private GameObject _onHitVFX;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger) return;
        
        var isIndestructible = other.CompareTag("Indestructible");
        if (isIndestructible)
        {
            ShowHitVfx();
            return;
        }
        
        var enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth && !_isEnemySource)
        {
            enemyHealth.TakeDamage(_damageAmount);
            ShowHitVfx();
            return;
        }
        
        var playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth && _isEnemySource)
        {
            playerHealth.TakeDamage(_damageAmount, transform);
            ShowHitVfx();
            return;
        }

    }

    private void ShowHitVfx()
    {
        if (!_onHitVFX) return;
        
        Instantiate(_onHitVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
