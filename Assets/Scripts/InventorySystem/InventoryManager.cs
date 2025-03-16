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
    [SerializeField] private ItemDropper _dropper;

    private int _selectedIndex=-1;
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
        _selectedItem.SetActive(false);
    }

    public void OnPressDrop()
    {
        if(_selectedIndex<0 || _inventory.Items[_selectedIndex]==null) return;
        _dropper.DropItem(_inventory.Items[_selectedIndex]);
        _inventory.RemoveItem(_selectedIndex);
    }

    public void OnPressEquip()
    {
        
    }

    public void OnSelectSlot(int indexSlot)
    {
        _selectedIndex = indexSlot;
        if (_inventory.Items[_selectedIndex] == null)
        {
            _selectedItem.SetActive(false);
            return;
        }
        else
        {
            _itemName.text = _inventory.Items[_selectedIndex].ItemName;
            _itemDescription.text = _inventory.Items[_selectedIndex].Description;
            _selectedItem.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].OnSlotSelected -= OnSelectSlot;
        }
        
        _inventory.SaveInventory();
    }
}
