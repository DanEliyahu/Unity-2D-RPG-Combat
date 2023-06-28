using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveInventory : MonoBehaviour
{
    private PlayerControls _playerControls;
    private WeaponInfo _weaponInfo;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void Start()
    {
        _playerControls.Inventory.ActiveWeapon.performed += ToggleActiveSlot;
        ToggleActiveHighlight(0);
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void ToggleActiveSlot(InputAction.CallbackContext callbackContext)
    {
        var activeSlotIndex = int.Parse(callbackContext.control.name) - 1;

        ToggleActiveHighlight(activeSlotIndex);
    }

    private void ToggleActiveHighlight(int index)
    {
        _weaponInfo = transform.GetChild(index).GetComponent<InventorySlot>().WeaponInfo;
        if (!_weaponInfo)
        {
            return;
        }
        
        foreach (Transform inventorySlot in transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        transform.GetChild(index).GetChild(0).gameObject.SetActive(true);
        ChangeActiveWeapon(index);
    }

    private void ChangeActiveWeapon(int index)
    {
        var weaponToSpawn = _weaponInfo._weaponPrefab;

        var newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform);
        ActiveWeapon.Instance.SetWeapon(newWeapon.GetComponent<Weapon>());
    }
}
