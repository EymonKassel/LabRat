using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour {
    [Header("Attributes")]
    [SerializeField]
    private float _movementSpeed = 5f;
    public int Health = 1;
    [SerializeField]
    private float _lineOfSite = 8f;
    [SerializeField]
    private float _attackRange = 4f;
    [SerializeField]
    private float _surrenderRange = 2f;

    [Header("References")]
    private Transform _player;

    private float _distanceFromPlayer;

    //[Header("References")]
    private Rigidbody2D _rb;

    private void Awake() {
        _rb = GetComponentInChildren<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<Transform>();
    }

    private void Update() {
        
        _distanceFromPlayer = Vector2.Distance(_player.position, transform.position);

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
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _lineOfSite);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
