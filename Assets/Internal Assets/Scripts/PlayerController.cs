using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    
    private AudioManager _audioManager;

    private IEnumerator _invulnerable;
    [SerializeField] private bool _isInvulnerable;
    [SerializeField] private float _invulnerableTimeDuration = 3f;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start() {
        CurrentHealth = _maxHealth;
        _canDash = true;
    }

    private void Update() {
        HandleInputs();
        HandleAnimations();

        if ( CurrentHealth <= 0 ) {
            Death();
        }

        if ( _isDashing ) {
            return;
        }

        if ( Input.GetMouseButtonDown(1) && _canDash ) {
            StartCoroutine(Dash());
        }
    }
    private void Death() {
        _audioManager.PlaySFX(_audioManager.PlayerDeath);
        //todo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TakeDamage() {
        if ( !_isInvulnerable ) {
            _audioManager.PlaySFX(_audioManager.PlayerTakingDamage);
            CurrentHealth--;
            _invulnerable = BeInvulnerable();
            StartCoroutine(_invulnerable);
        }
    }

    private IEnumerator BeInvulnerable() {
        _isInvulnerable = true;
        yield return new WaitForSeconds(_invulnerableTimeDuration);
        _isInvulnerable = false;
    }

    private void FixedUpdate() {
        if ( _isDashing ) {
            return;
        }
        Move();
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
        
        _canDash = false;
        _isDashing = true;
        _dashTrailsPrefab.SetActive(true);
        Vector2 _directionToMouseNormalized = _directionToMouse.normalized;
        _rb.velocity = new Vector2(_directionToMouseNormalized.x * _dashSpeed, _directionToMouseNormalized.y * _dashSpeed);
        _audioManager.PlaySFX(_audioManager.PlayerDash);
        yield return new WaitForSeconds(_dashDuration);
        _isDashing = false;
        _dashTrailsPrefab.SetActive(false);

        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
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
