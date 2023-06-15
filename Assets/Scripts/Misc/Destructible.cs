using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject _destroyVFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<DamageSource>())
        {
            Instantiate(_destroyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
