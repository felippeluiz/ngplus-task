using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public event Action<int> OnSlotSelected; 
    [SerializeField] private DraggableItem _prefabDraggable;
    private int _inventoryIndex;
    private InventorySO _inventory;
    private DraggableItem _myDraggable;
    

    public void OnDrop(PointerEventData eventData)
    {
        if (_inventory.Items[_inventoryIndex] != null) return;
        GameObject droppedItem = eventData.pointerDrag;
        if (droppedItem.TryGetComponent(out DraggableItem draggableItem))
        {
            draggableItem.SetInventorySlot(this);
        }

        _inventory.Items[_inventoryIndex] = draggableItem.Item;
        _myDraggable = draggableItem;
    }

    public void StartSlot(InventorySO inventory, int index)
    {
        _inventoryIndex = index;
        _inventory = inventory;
        _inventory.OnInventoryUpdated += OnInventoryUpdate;

        if (_inventory.Items[_inventoryIndex] == null) return;
        AddedItem();
    }

    public void RemovedItem()
    {
        _inventory.Items[_inventoryIndex] = null;
        _myDraggable = null;
    }

    private void DroppedItem()
    {
        Destroy(_myDraggable.gameObject);
        _myDraggable = null;
    }

    private void OnInventoryUpdate(int index)
    {
        if (index != _inventoryIndex) return;

        if (_inventory.Items[_inventoryIndex] == null)
            DroppedItem();
        else
        {
            AddedItem();
        }
    }

    private void AddedItem()
    {
        var newDraggable = Instantiate(_prefabDraggable, transform);
        newDraggable.SetItem(_inventory.Items[_inventoryIndex]);
        newDraggable.SetInventorySlot(this);
        _myDraggable = newDraggable;
    }
    
    private void OnDestroy()
    {
        _inventory.OnInventoryUpdated -= OnInventoryUpdate;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSlotSelected?.Invoke(_inventoryIndex);
    }
}