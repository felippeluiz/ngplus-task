using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int SpeedId = Animator.StringToHash("speed");
    [SerializeField] private float _speed=1f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private Vector2 _moveAmount;
    private bool _facingRight = true;

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveAmount = context.ReadValue<Vector2>();
        if (_moveAmount.x != 0)
        {
            _facingRight = _moveAmount.x > 0;
        }
    }

    private void FixedUpdate()
    {
        _spriteRenderer.flipX = !_facingRight;
        _animator.SetFloat(SpeedId,_moveAmount.magnitude);
        _rb.MovePosition(_rb.position+_moveAmount * (_speed * Time.fixedDeltaTime));
    }
}
