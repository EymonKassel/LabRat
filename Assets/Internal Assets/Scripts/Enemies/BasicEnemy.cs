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

    [Header("References")]
    private Transform _player;

    private float _distanceFromPlayer;

    private Rigidbody2D _rb;

    private void Awake() {
        _rb = GetComponentInChildren<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Start() {
        _currentHealth = MaxHealth;
    }

    private void Update() {
        _distanceFromPlayer = Vector2.Distance(_player.position, transform.position);

        if ( _currentHealth <= 0 ) {
            Destroy(gameObject);
        }

        if ( _distanceFromPlayer < _attackRange ) {
            Attack();
        } else {
            Follow();
        }
    }
    private void Attack() {
        Debug.Log("attacking");
    }
    private void Follow() {
        if ( _distanceFromPlayer < _lineOfSite ) {
            transform.position = Vector2.MoveTowards(transform.position, _player.position, _movementSpeed * Time.deltaTime);
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
