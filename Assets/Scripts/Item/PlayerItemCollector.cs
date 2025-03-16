using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerItemCollector : MonoBehaviour
{
    [SerializeField] private InventorySO _inventory;
    
    private List<CollectableItem> _toCollect = new List<CollectableItem>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag(TagManager.Item)) return;
        
        if(!other.gameObject.TryGetComponent(out CollectableItem collectableItem)) return;
        
        if(_toCollect.Contains(collectableItem)) return;
        
        AddItemToCollect(collectableItem);
    }

    private void AddItemToCollect(CollectableItem collectableItem)
    {
        Debug.Log("Added item to collect");
        _toCollect.Add(collectableItem);
    }
    
    public void TryToCollect(InputAction.CallbackContext context)
    {
        if(_toCollect.Count<=0) return;

        if (_inventory.TryAddItem(_toCollect[0].Item))
        {
            Destroy(_toCollect[0].gameObject);
            _toCollect.RemoveAt(0);
        }
    }
}
