using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "InventorySO", menuName = "Scriptable Objects/InventorySO")]
public class InventorySO : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private List<ItemSO> _items = new List<ItemSO>();
    
    private ItemSO[] _runtimeItems;
    public event Action<int> OnInventoryUpdated;
    
    public ItemSO[] Items => _runtimeItems;
    public void InitializeInventory(int size)
    {
        _runtimeItems = new ItemSO[size];
        for (int i = 0; i < _items.Count; i++)
        {
            if(i>=size) break;

            _runtimeItems[i] = _items[i];
        }
    }

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
     
    }

    public bool AddItem(ItemSO newItem, int slotIndex)
    {
        if (_runtimeItems[slotIndex] != null) return false; // Slot is occupied
        
        _runtimeItems[slotIndex] = newItem;
        OnInventoryUpdated?.Invoke(slotIndex);
        return true;

    }

    public void RemoveItem(int slotIndex)
    {
        if ((slotIndex < 0 || slotIndex >= _runtimeItems.Length) && _runtimeItems[slotIndex]!=null) return;
        
        _runtimeItems[slotIndex] = null;
        OnInventoryUpdated?.Invoke(slotIndex);
    }

    public bool TryAddItem(ItemSO item)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null)
            {
                AddItem(item, i);
                return true;
            }
        }

        return false;
    }
}