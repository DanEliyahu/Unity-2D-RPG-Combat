using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _startingHealth = 3;

    private int _currentHealth;
    private Knockback _knockBack;

    private void Awake()
    {
        _currentHealth = _startingHealth;
        _knockBack = GetComponent<Knockback>();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _knockBack.GetKnockedBack(PlayerController.Instance.transform, 15f);
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
