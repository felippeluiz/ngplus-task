using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerItemCollector : MonoBehaviour
{
    [SerializeField] private InventorySO _inventory;
    [SerializeField] private GameObject _textInventoryFull;
    
    private List<CollectableItem> _toCollect = new List<CollectableItem>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag(TagManager.Item)) return;
        
        if(!other.gameObject.TryGetComponent(out CollectableItem collectableItem)) return;
        
        if(_toCollect.Contains(collectableItem)) return;
        
        collectableItem.ChangeOnRangeToCollect(true);
        AddItemToCollect(collectableItem);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag(TagManager.Item)) return;
        
        if(!other.gameObject.TryGetComponent(out CollectableItem collectableItem)) return;
        
        if(!_toCollect.Contains(collectableItem)) return;
        
        collectableItem.ChangeOnRangeToCollect(false);
        _toCollect.Remove(collectableItem);
    }

    private void AddItemToCollect(CollectableItem collectableItem)
    {
        _toCollect.Add(collectableItem);
    }
    
    public void TryToCollect(InputAction.CallbackContext context)
    {
        if(_toCollect.Count<=0) return;

        if (_inventory.TryAddItem(_toCollect[0].Item))
        {
            var toBeDestroyed = _toCollect[0]; 
            _toCollect.Remove(toBeDestroyed);
            Destroy(toBeDestroyed.gameObject);
        }
        else
        {
            if(_textInventoryFull.activeInHierarchy) return;
            
            _textInventoryFull.SetActive(true);
            StartCoroutine(DelayedDisableTextFull());
        }
    }

    private IEnumerator DelayedDisableTextFull()
    {
        yield return new WaitForSeconds(2f);
        _textInventoryFull.SetActive(false);
    }
}
