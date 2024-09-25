using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public int CurrentHealth = 1;
    [SerializeField]
    private int _maxHealth = 1;
    [SerializeField]
    private float _movementSpeed = 5f;
    [SerializeField] private Animator _controller;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private SpriteRenderer _Rhand;
    [SerializeField] private SpriteRenderer _Lhand;
    [SerializeField] private SpriteRenderer _gun;
    bool _flipped = false;

    private Vector2 _position;
    private Rigidbody2D _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update() {
        HandleInputs();
        HandleAnimations();
    }
    private void FixedUpdate() {
        Move();
    }
    private void HandleInputs() {
        _position.x = Input.GetAxisRaw("Horizontal");
        _position.y = Input.GetAxisRaw("Vertical");
    }
    private void Move() {
        // Make better later
        _rb.velocity = _movementSpeed * Time.fixedDeltaTime * _position.normalized;
    }

    private void HandleAnimations()
    {
        _controller.SetFloat("Speed", _rb.velocity.magnitude);
        if(_position.x < 0 && !_flipped)
        {
            _flipped = true;
            _Rhand.sortingOrder = -1;
            _Lhand.sortingOrder = 1;
            _gun.sortingOrder = -2;
            _sprite.transform.localScale = new Vector2(-transform.localScale.x,transform.localScale.y);
            _Rhand.transform.localScale = new Vector2(-transform.localScale.x,transform.localScale.y);;
        }
        if (_position.x > 0 && _flipped)
        {
            _flipped = false;
            _Rhand.sortingOrder = 1;
            _Lhand.sortingOrder = -1;
            _gun.sortingOrder = 2;
            _sprite.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
            _Rhand.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        }
    }
}
