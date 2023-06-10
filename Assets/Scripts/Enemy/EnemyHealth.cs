using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _startingHealth = 3;
    [SerializeField] private ParticleSystem _deathVFX;
    [SerializeField] private float _knockBackThrust = 15f;

    private int _currentHealth;
    private Knockback _knockBack;
    private Flash _flash;

    private void Awake()
    {
        _currentHealth = _startingHealth;
        _knockBack = GetComponent<Knockback>();
        _flash = GetComponent<Flash>();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _knockBack.GetKnockedBack(PlayerController.Instance.transform, _knockBackThrust);
        StartCoroutine(_flash.FlashRoutine(DetectDeath));
    }

    private void DetectDeath()
    {
        if (_currentHealth > 0) return;
        Instantiate(_deathVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
