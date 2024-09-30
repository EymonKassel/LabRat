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
    [SerializeField]
    protected float RetreatRange = 2f;
    [SerializeField]
    protected float _cooldown = 1f;
    protected float DistanceFromPlayer;
    protected Vector3 Direction = Vector3.right;
    protected float lastAttackTime = 1f;
    protected bool isAlive;

    [Header("References")]
    protected PlayerController PlayerController;
    protected Transform PlayerPosition;
    [SerializeField]
    protected Rigidbody2D Rb;
    [SerializeField]
    protected Animator Animator;

    public AudioManager AudioManager;

    protected virtual void OnEnable() {
        AudioManager = FindObjectOfType<AudioManager>();        
        PlayerController = FindAnyObjectByType<PlayerController>();
        PlayerPosition = PlayerController.transform;
        CurrentHealth = MaxHealth;
        AudioManager.PlaySFX(AudioManager.EnemyBirth);
        isAlive = true;
    }

    private void Update() 
    {
        if ( CurrentHealth <= 0 ) {
            isAlive = false;
            Animator.SetBool("IsDead", true);
        }
    }

    private void FixedUpdate()
    {
        if(isAlive)
            Movement();
    }

    public void Death() {
        Destroy(gameObject);
        FindAnyObjectByType<Wave>().SendMessage("EnemyDied");
    }
    private void GetDistanceFromPlayer() {
        DistanceFromPlayer = Vector2.Distance(PlayerPosition.position, transform.position);
    }

    protected virtual void GetDirectionToPlayer()
    {
        Direction = (PlayerController.transform.position - transform.position).normalized;
    }

    protected virtual void Movement()
    {
        GetDistanceFromPlayer();
        GetDirectionToPlayer();

        if (DistanceFromPlayer < AttackRange)
        {
            if (Time.time - lastAttackTime > _cooldown)
                Attack();
        }
        else if (DistanceFromPlayer < RetreatRange)
            Rb.MovePosition(transform.position + 2f * -MovementSpeed * Time.fixedDeltaTime * Direction);
        else
            Rb.MovePosition(transform.position + MovementSpeed * Time.fixedDeltaTime * Direction);
    }

    protected virtual void Attack()
    {

    }

    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        if ( collision.gameObject.GetComponent<BasicBullet>() != null ) {
            CurrentHealth--;
        }
    }

    protected virtual void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, RetreatRange);
    }
}
