using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private string _itemName;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Sprite _dropped;

    public string ItemName => _itemName;
    public Sprite Icon => _icon;
    public Sprite Dropped => _dropped;
    public string Description => _description;
}
