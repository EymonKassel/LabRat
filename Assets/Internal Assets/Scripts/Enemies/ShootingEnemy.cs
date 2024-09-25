using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour {
    [Header("Attributes")]
    [SerializeField]
    private float _movementSpeed = 5f;
    private int _currentHealth = 1;
    public int MaxHealth = 1;
    [SerializeField]
    private float _lineOfSite = 8f;
    [SerializeField]
    private float _attackRange = 4f;
    [SerializeField]
    private float _surrenderRange = 2f;

    [Header("References")]
    private Transform _playerPosition;

    private float _distanceFromPlayer;
    private bool _isAttacking = false;
    public int AttackPower = 1;

    //[Header("References")]
    private Rigidbody2D _rb;
    private PlayerController _playerController;
    private void Start() {
        _currentHealth = MaxHealth;
    }
    private void Awake() {
        _rb = GetComponentInChildren<Rigidbody2D>();
        _playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _playerController = FindAnyObjectByType<PlayerController>();
    }

    private void Update() {
        if ( _currentHealth <= 0 ) {
            Die();
        }
        _distanceFromPlayer = Vector2.Distance(_playerPosition.position, transform.position);

        if ( _distanceFromPlayer < _surrenderRange ) {
            Surrender();

        } else if (_distanceFromPlayer < _attackRange) {
            Attack();

        } else {
            Follow();
        }
    }
    private IEnumerator Attack() {
        _isAttacking = true;
        //_animator.SetBool("IsAttacking", true);
        _playerController.CurrentHealth -= AttackPower;
        yield return new WaitForSeconds(1);
        _isAttacking = false;
        //_animator.SetBool("IsAttacking", false);
    }
    private void Die() {
        // Improve later
        Destroy(gameObject);
    }
    private void Surrender() {
        if ( _distanceFromPlayer < _surrenderRange ) {
            Vector3 direction = ( _playerController.transform.position - transform.position ).normalized;
            _rb.MovePosition(transform.position + direction * -_movementSpeed * 2f * Time.deltaTime);
        }
    }
    private void Follow() {
        if ( _distanceFromPlayer < _lineOfSite ) {
            Vector3 direction = ( _playerController.transform.position - transform.position ).normalized;
            _rb.MovePosition(transform.position + direction * _movementSpeed * Time.deltaTime);
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
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _surrenderRange);
    }
}
