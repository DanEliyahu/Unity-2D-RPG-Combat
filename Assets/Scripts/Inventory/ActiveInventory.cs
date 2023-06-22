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
        var activeSlotIndexNum = int.Parse(callbackContext.control.name) - 1;

        foreach (Transform inventorySlot in transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        transform.GetChild(activeSlotIndexNum).GetChild(0).gameObject.SetActive(true);
    }
}
