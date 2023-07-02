using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float _attackCooldown = 0.5f;

    public float AttackCooldown => _attackCooldown;

    public abstract void Attack();

    protected void FaceMouse()
    {
        var mousePosition = CameraController.Instance.MainCam.ScreenToWorldPoint(Input.mousePosition);

        var activeWeaponTransform = ActiveWeapon.Instance.transform;
        Vector2 direction = mousePosition - activeWeaponTransform.position;
        activeWeaponTransform.right = direction;
    }
    
    protected void MouseFollowWithOffset()
    {
        var mousePos = Input.mousePosition;
        var mouseWorldPosition = CameraController.Instance.MainCam.ScreenToWorldPoint(mousePos);
        
        var zRotation = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        var yRotation = mouseWorldPosition.x < PlayerController.Instance.transform.position.x ? 180 : 0;
        
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, yRotation, zRotation);
    }
}
