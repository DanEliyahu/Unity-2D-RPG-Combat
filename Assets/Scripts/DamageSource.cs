using UnityEngine;

public class DamageSource : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyAI>())
        {
            Debug.Log("Hit");
        }
    }
}
