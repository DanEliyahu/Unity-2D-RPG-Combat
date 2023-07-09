using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private float _knockBackThrust = 10f;
    [SerializeField] private float _damageRecoveryTime = 1f;

    private int _currentHealth;
    private bool _canTakeDamage = true;
    private Knockback _knockback;
    private Flash _flash;

    private void Awake()
    {
        _knockback = GetComponent<Knockback>();
        _flash = GetComponent<Flash>();
        _currentHealth = _maxHealth;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!_canTakeDamage) return;
        
        var enemy = other.gameObject.GetComponent<EnemyAI>();
        if (!enemy) return;
        
        TakeDamage(1);
        _knockback.GetKnockedBack(other.transform, _knockBackThrust);
        StartCoroutine(_flash.FlashRoutine());
    }

    private void TakeDamage(int damageAmount)
    {
        _canTakeDamage = false;
        _currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(_damageRecoveryTime);
        _canTakeDamage = true;
    }
}
