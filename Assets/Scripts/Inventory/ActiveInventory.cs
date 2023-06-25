using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveInventory : MonoBehaviour
{
    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void Start()
    {
        _playerControls.Inventory.ActiveWeapon.performed += ToggleActiveSlot;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void ToggleActiveSlot(InputAction.CallbackContext callbackContext)
    {
        var activeSlotIndex = int.Parse(callbackContext.control.name) - 1;

        foreach (Transform inventorySlot in transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        transform.GetChild(activeSlotIndex).GetChild(0).gameObject.SetActive(true);
        ChangeActiveWeapon(activeSlotIndex);
    }

    private void ChangeActiveWeapon(int index)
    {
        Debug.Log(transform.GetChild(index).GetComponent<InventorySlot>().WeaponInfo._weaponPrefab.name);
    }
}
