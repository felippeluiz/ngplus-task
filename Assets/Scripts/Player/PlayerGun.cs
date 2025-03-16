using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Util;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer; 
    [Header("Bullet spawn")]
    [SerializeField] private Bullet _prefabBullet;
    [SerializeField] private Transform _spawnPoint;
    private ItemSO _equippedItem = null;
    private GameObjectPool<Bullet> _bulletsPool;
    private bool _canShoot = true;
    private Vector2 _mousePosition;
    private InputAction _mouseAim;
    
    private void Awake()
    {
        _bulletsPool = new GameObjectPool<Bullet>(_prefabBullet);
        _mouseAim = InputSystem.actions.FindAction("Aim");
    }
    private void Start()
    {
        
    }

    private void OnEnable()
    {
        _mouseAim.performed += UpdateMousePosition;
    }

    private void OnDisable()
    {
        _mouseAim.performed -= UpdateMousePosition;
    }

    private void UpdateMousePosition(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(_mousePosition.x, _mousePosition.y, Camera.main.nearClipPlane));
        Vector2 direction = (mouseWorldPosition - _spawnPoint.position).normalized;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void EquipItem(ItemSO item)
    {
        _equippedItem = item;
        _spriteRenderer.sprite = _equippedItem.Equipped;
        _spriteRenderer.gameObject.SetActive(true);
    }

    public void DroppedItem(ItemSO item)
    {
        if(_equippedItem!=item) return;

        _equippedItem = null;
        _spriteRenderer.sprite = null;
        _spriteRenderer.gameObject.SetActive(false);
    }

    public void PressAttack(InputAction.CallbackContext context)
    {
        if(_equippedItem==null) return;
        if (_canShoot)
            TryToShoot();
    }

    private void TryToShoot()
    {
        var direction = _spawnPoint.position-transform.position;
        Shoot(direction);
    }

    private void Shoot(Vector3 direction)
    {
        var bullet = _bulletsPool.Get();
        bullet.transform.position = _spawnPoint.position;
        bullet.OnDeath += () => _bulletsPool.Release(bullet);
        direction.Normalize();
        bullet.SetBullet(_equippedItem.Projectile, direction,_equippedItem.ProjectLifetime,_equippedItem.ProjectSpeed);
        _canShoot = false;
        StartCoroutine(DelayToShot());
    }

    private IEnumerator DelayToShot()
    {
        yield return new WaitForSeconds(1 / _equippedItem.FireRate);
        _canShoot = true;
    }
}
