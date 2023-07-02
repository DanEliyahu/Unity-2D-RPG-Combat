using UnityEngine;

public class Staff : Weapon
{
    public override void Attack()
    {
        Debug.Log("Staff Attack");
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }
}
