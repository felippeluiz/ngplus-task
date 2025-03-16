using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventorySO _inventory;
    [SerializeField] private InventorySlot[] _slots;
    [SerializeField] private DraggableItem _prefabDraggable;

    void Start()
    {
        _inventory.InitializeInventory(_slots.Length);

        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].StartSlot(_inventory,i);
        }
    }

    public void OnPressInventoryButton()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
