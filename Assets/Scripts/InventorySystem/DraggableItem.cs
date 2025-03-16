using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    private ItemSO _item;
    private Image _image;
    private InventorySlot _slot;
    public ItemSO Item => _item;
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_slot.transform);
        _image.raycastTarget = true;
        transform.localPosition = Vector3.zero;
    }

    public void SetInventorySlot(InventorySlot slot)
    {
        if (_slot!=null && slot != _slot)
        {
            _slot.RemovedItem();
        }
        _slot = slot;
    }

    public void SetItem(ItemSO item)
    {
        _item = item;
        _image.sprite = _item.Icon;
    }
}
