using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int _damageAmount = 1;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth)
        {
            enemyHealth.TakeDamage(_damageAmount);
        }
    }
}
