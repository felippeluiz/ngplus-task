using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private int _itemID;
    [SerializeField] private string _itemName;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Sprite _dropped;
    [SerializeField] private Sprite _equipped;
    [SerializeField] private Sprite _projectile;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _projectLifetime;
    [SerializeField] private float _projectSpeed;
    

    public int ItemID => _itemID;
    public string ItemName => _itemName;
    public string Description => _description;
    public Sprite Icon => _icon;
    public Sprite Dropped => _dropped;
    public Sprite Equipped => _equipped;
    public Sprite Projectile => _projectile;
    public float FireRate => _fireRate;
    public float ProjectLifetime => _projectLifetime;
    public float ProjectSpeed => _projectSpeed;
}
