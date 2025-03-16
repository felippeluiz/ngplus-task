using System;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventorySO _inventory;
    [SerializeField] private InventorySlot[] _slots;
    [SerializeField] private GameObject _inventoryVisual;
    [SerializeField] private GameObject _selectedItem;
    [SerializeField] private TextMeshProUGUI _itemName, _itemDescription;
    
    void Awake()
    {
        _inventory.InitializeInventory(_slots.Length);

        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].StartSlot(_inventory,i);
            _slots[i].OnSlotSelected += OnSelectSlot;
        }
    }

    public void OnPressInventoryButton()
    {
        _inventoryVisual.SetActive(!_inventoryVisual.activeSelf);
    }

    public void OnPressDrop()
    {
        
    }

    public void OnPressEquip()
    {
        
    }

    public void OnSelectSlot(int indexSlot)
    {
        if (_inventory.Items[indexSlot] == null)
        {
            _selectedItem.SetActive(false);
            return;
        }
        else
        {
            _itemName.text = _inventory.Items[indexSlot].ItemName;
            _itemDescription.text = _inventory.Items[indexSlot].Description;
            _selectedItem.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].OnSlotSelected -= OnSelectSlot;
        }
    }
}
