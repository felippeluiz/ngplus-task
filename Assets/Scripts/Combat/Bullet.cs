using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    public event Action OnDeath;
    
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private float _totalLifeTime = 5f;
    private float _speed = 1f;
    
    private int _damage = 1;
    private float _lifeTime = 0f;
    private Vector3 _direction= Vector3.forward;
    
    private void OnEnable()
    {
        _lifeTime = 0f;
    }

    void Update()
    {
        _lifeTime += Time.deltaTime;
        if (_lifeTime > _totalLifeTime)
            Die();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(transform.position + _direction * (_speed * Time.fixedDeltaTime));
    }

    public void SetBullet(Sprite projectile, Vector3 dir, float lifeTime, float speed)
    {
        _spriteRenderer.sprite = projectile;
        _direction = dir;
        _totalLifeTime = lifeTime;
        _speed = speed;

        if (TryGetComponent(out Collider2D collider))
        {
            Destroy(collider);
            StartCoroutine(LateAddCollider());
        }
    }

    IEnumerator LateAddCollider()
    {
        yield return new WaitForEndOfFrame();
        gameObject.AddComponent<PolygonCollider2D>();
    }

    private void Die()
    {
        OnDeath?.Invoke();
        OnDeath = null;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(TagManager.Enemy)) return;

        if (!other.gameObject.TryGetComponent<Enemy>(out var enemy)) return;

        enemy.TakeDamage(_damage);

        Die();
    }
}