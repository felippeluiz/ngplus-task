using System;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField]private ItemSO _item;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public ItemSO Item => _item;

    private void Awake()
    {
        _spriteRenderer.sprite = _item.Dropped;
    }
}
