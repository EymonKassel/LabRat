using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

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

    private bool _isAttacking;
    private IEnumerator _attackCoroutine;
    [SerializeField] EnemySpawn[] spawns;

    private void Awake() {
        _rb = GetComponentInChildren<Rigidbody2D>();
        _playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void Start() {
        _currentHealth = MaxHealth;
    }

    private void Update() {
        _distanceFromPlayer = Vector2.Distance(_playerPosition.position, transform.position);

        if ( _currentHealth <= 0 ) {
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
        _playerController.CurrentHealth -= AttackPower;
        yield return new WaitForSeconds(1);
        _isAttacking = false;
    }
    private void Follow() {
        Vector3 direction = (_playerController.transform.position - transform.position).normalized;
        _rb.MovePosition(transform.position + direction * _movementSpeed * Time.deltaTime);
    }
  
    private void OnCollisionEnter2D(Collision2D collision) {
        if ( collision.gameObject.GetComponent<BasicBullet>() != null ) {
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

[Serializable]
public class EnemySpawn
{
    public BasicEnemy enemy;
    public Vector2 position;
}
