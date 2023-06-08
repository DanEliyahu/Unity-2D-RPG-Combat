using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _startingHealth = 3;

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
        _knockBack.GetKnockedBack(PlayerController.Instance.transform, 15f);
        StartCoroutine(_flash.FlashRoutine(DetectDeath));
    }

    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
