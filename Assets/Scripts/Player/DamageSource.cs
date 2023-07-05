using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int _damageAmount = 1;
    [SerializeField] private GameObject _onHitVFX;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemyHealth = other.GetComponent<EnemyHealth>();
        var isIndestructible = other.CompareTag("Indestructible");
        
        // Collision with non trigger object which is either an enemy or Indestructible
        if (!other.isTrigger && (enemyHealth || isIndestructible))
        {
            if (enemyHealth)
            {
                enemyHealth.TakeDamage(_damageAmount);
            }
            if (_onHitVFX)
            {
                Instantiate(_onHitVFX, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
