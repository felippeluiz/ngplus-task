using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "InventorySO", menuName = "Scriptable Objects/InventorySO")]
public class InventorySO : ScriptableObject
{
    [SerializeField] private List<ItemSO> _defaultInventory = new List<ItemSO>();
    [SerializeField] private List<ItemSO> _allItems = new List<ItemSO>();
    
    private ItemSO[] _runtimeItems;
    public event Action<int> OnInventoryUpdated;
    public ItemSO[] Items => _runtimeItems;
    
    private static readonly string NameSave = "inventory.json";
    public void InitializeInventory(int size)
    {
        _runtimeItems = new ItemSO[size];
        for (int i = 0; i < _defaultInventory.Count; i++)
        {
            if(i>=size) break;

            _runtimeItems[i] = _defaultInventory[i];
        }
        
        LoadInventory();
    }

    public bool AddItem(ItemSO newItem, int slotIndex)
    {
        if (_runtimeItems[slotIndex] != null) return false; // Slot is occupied
        
        _runtimeItems[slotIndex] = newItem;
        OnInventoryUpdated?.Invoke(slotIndex);
        return true;
        SaveInventory();
    }

    public void RemoveItem(int slotIndex)
    {
        if ((slotIndex < 0 || slotIndex >= _runtimeItems.Length) && _runtimeItems[slotIndex]==null) return;
        
        _runtimeItems[slotIndex] = null;
        OnInventoryUpdated?.Invoke(slotIndex);
        
        SaveInventory();
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
    
    public void SaveInventory()
    {
        InventorySaveData saveData = new InventorySaveData(_runtimeItems);
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, NameSave), json);
    }
    
    private void LoadInventory()
    {
        if (!File.Exists(Path.Combine(Application.persistentDataPath, NameSave))) return;
        
        string json = File.ReadAllText(Path.Combine(Application.persistentDataPath, NameSave));
        InventorySaveData saveData = JsonUtility.FromJson<InventorySaveData>(json);
        
        for (int i = 0; i < saveData.items.Length; i++)
        {
            _runtimeItems[i] = saveData.items[i] > 0 ? _allItems.FirstOrDefault(item => item.ItemID == saveData.items[i]) : null;
        }
    }
}
[Serializable]
public class InventorySaveData
{
    public int[] items;

    public InventorySaveData(ItemSO[] inventory)
    {
        items = new int[inventory.Length];
        for (int i = 0; i < inventory.Length; i++)
        {
            items[i] = inventory[i] != null ? inventory[i].ItemID : -1;
        }
    }
}