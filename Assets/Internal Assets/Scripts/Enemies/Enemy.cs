using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header("Attributes")]
    [SerializeField] 
    protected int MaxHealth = 1;
    protected int CurrentHealth;
    [SerializeField]
    protected float MovementSpeed = 5f;
    [SerializeField]
    protected float AttackRange = 1f;
    protected float DistanceFromPlayer;

    [Header("References")]
    protected PlayerController PlayerController;
    protected Transform PlayerPosition;
    [SerializeField]
    protected Rigidbody2D Rb;
    [SerializeField]
    protected Animator Animator;

    private void Awake() {
        PlayerController = FindAnyObjectByType<PlayerController>();
        PlayerPosition = PlayerController.transform;
    }
    private void Start() {
        CurrentHealth = MaxHealth;
    }
    protected virtual void Update() {
        GetDistanceFromPlayer();
        if ( CurrentHealth <= 0 ) {
            Animator.SetBool("IsDead", true);
        }
    }
    public void Death() {
        Destroy(gameObject);
        FindAnyObjectByType<Wave>().SendMessage("EnemyDied");
    }
    private void GetDistanceFromPlayer() {
        DistanceFromPlayer = Vector2.Distance(PlayerPosition.position, transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if ( collision.gameObject.GetComponent<BasicBullet>() != null ) {
            CurrentHealth--;
        }
    }

    protected virtual void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
