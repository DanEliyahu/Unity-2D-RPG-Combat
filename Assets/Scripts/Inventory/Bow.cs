using UnityEngine;

public class Bow : Weapon
{
    public override void Attack()
    {
        Debug.Log("Bow Attack");
    }

    private void Update()
    {
        FaceMouse();
    }
}
