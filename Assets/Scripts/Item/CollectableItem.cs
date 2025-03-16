using System;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private ItemSO _item;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject _textCollect;
    [SerializeField] private float _rotateSpeed = 10f;
    public ItemSO Item => _item;

    private void Awake()
    {
        SetSprite();
    }

    public void ChangeOnRangeToCollect(bool isOnRange)
    {
        _textCollect.SetActive(isOnRange);
    }

    private void Update()
    {
        _spriteRenderer.transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }

    public void SetItem(ItemSO item)
    {
        _item = item;
        SetSprite();
    }

    private void SetSprite()
    {
        _spriteRenderer.sprite = _item.Dropped;
    }
}