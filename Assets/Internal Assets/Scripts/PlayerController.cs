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

    [SerializeField] private GameObject _dashTrailsPrefab;
    [SerializeField] private float _dashSpeed = 100f;
    [SerializeField] private float _dashDuration = 1f;
    [SerializeField] private float _dashCooldown = 1f;
    private bool _isDashing;
    private bool _canDash;
    private Vector2 _directionToMouse;
    private float _lastDashTime = float.MinValue;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        HandleInputs();
        HandleAnimations();

        if ( Input.GetMouseButtonDown(1) && _canDash ) {
            _canDash = true;
        }
    }
    private void FixedUpdate() {
        
        if ( _canDash  ) {
            StartCoroutine(Dash());
        }
        if ( !_isDashing ) {
            Move();

        }
    }
    private void HandleInputs() {
        _position.x = Input.GetAxisRaw("Horizontal");
        _position.y = Input.GetAxisRaw("Vertical");
        _directionToMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        

    }
    private void Move() {
        // Make better later
        _rb.velocity = _movementSpeed * Time.fixedDeltaTime * _position.normalized;
    }

    private IEnumerator Dash() {
        _isDashing = true;
        _rb.velocity = _directionToMouse.normalized * _dashSpeed;
        yield return new WaitForSeconds(_dashDuration);
        _lastDashTime = Time.time;
        _isDashing = false;
        _canDash = false;
    }

    private void HandleAnimations() {
        _controller.SetFloat("Speed", _rb.velocity.magnitude);
        if ( _position.x < 0 && !_flipped ) {
            _flipped = true;
            _Rhand.sortingOrder = -1;
            _Lhand.sortingOrder = 1;
            _gun.sortingOrder = -2;
            _sprite.transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            _Rhand.transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y); ;
        }
        if ( _position.x > 0 && _flipped ) {
            _flipped = false;
            _Rhand.sortingOrder = 1;
            _Lhand.sortingOrder = -1;
            _gun.sortingOrder = 2;
            _sprite.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
            _Rhand.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        }
    }
}
