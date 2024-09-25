using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {
    [Header("Attributes")]
    [SerializeField]
    private float _movementSpeed = 5f;
    public int MaxHealth = 1;
    private int _currentHealth;
    [SerializeField]
    private float _lineOfSite = 5f;
    [SerializeField]
    private float _attackRange = 1f;
    public int AttackPower = 1;

    [Header("References")]
    private Transform _playerPosition;
    private PlayerController _playerController;

    private float _distanceFromPlayer;
    private Rigidbody2D _rb;
    private Animator _animator;

    private bool _isAttacking;
    private IEnumerator _attackCoroutine;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
    }
    private void Start() {
        _currentHealth = MaxHealth;
    }

    private void Update() {
        _distanceFromPlayer = Vector2.Distance(_playerPosition.position, transform.position);

        if (_rb.velocity.magnitude <= 0.1f) {
            _animator.SetBool("IsMoving", false);
        }

        if ( _currentHealth <= 0 ) {
            // Improve later
            _animator.SetBool("IsDead", true);
            Destroy(gameObject);
        }

        if ( _distanceFromPlayer < _attackRange  ) {
            if ( !_isAttacking ) {
                _attackCoroutine = Attack();
                StartCoroutine(_attackCoroutine);
            }
            
        } else {
            Follow();
        }
    }
    private IEnumerator Attack() {
        _isAttacking = true;
        _animator.SetBool("IsAttacking", true);
        _playerController.CurrentHealth -= AttackPower;
        yield return new WaitForSeconds(1);
        _isAttacking = false;
        _animator.SetBool("IsAttacking", false);
    }
    private void Follow() {
        if ( _distanceFromPlayer < _lineOfSite ) {
            _animator.SetBool("IsMoving", true);
            transform.position = Vector2.MoveTowards(transform.position, _playerPosition.position, _movementSpeed * Time.deltaTime);
        }
    }
  
    private void OnCollisionEnter2D(Collision2D collision) {
        if ( collision.gameObject.layer == 10 ) {
            Debug.Log("ouch");
            _currentHealth--;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _lineOfSite);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
