using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Info")]
public class WeaponInfo : ScriptableObject
{
    public GameObject _weaponPrefab;
    public float _attackCooldown;
}
