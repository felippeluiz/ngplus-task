using System;
using System.Linq;
using UnityEngine;

public class ColletablesLoader : MonoBehaviour
{
    [SerializeField] private CollectableItem[] _collectableItems;
    [SerializeField] private InventorySO _inventory;

    private void Start()
    {
        foreach (var collectableItem in _collectableItems)
        {
            collectableItem.gameObject
                .SetActive(!_inventory.Items.Any(item => item!=null && item.ItemID == collectableItem.Item.ItemID));
        }
    }
}
