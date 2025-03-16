using Unity.Mathematics;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    [SerializeField] private CollectableItem _collectableItem;
    public void DropItem(ItemSO item)
    {
        var collectableItem = Instantiate(_collectableItem,transform.position,quaternion.identity).GetComponent<CollectableItem>();
        collectableItem.SetItem(item);
    }
}
