using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingEnemy : Enemy {
    [SerializeField]
    protected float RetreatRange = 2f;
    [SerializeField]
    protected GameObject BulletPrefab;
    [SerializeField]
    protected float _cooldown = 1f;
    [SerializeField]
    protected float _bulletForce = 20f;
    protected Rigidbody2D BulletPrefabRB;

    protected bool IsAttacking;
    protected IEnumerator AttackCoroutine;
    protected Vector3 Direction;

    protected override void Update() {
        base.Update();
        GetDirectionToPlayer();
        if (DistanceFromPlayer < RetreatRange)
            Retreat();
        else if (DistanceFromPlayer < AttackRange)
        {
            if (!IsAttacking)
            {
                AttackCoroutine = Attack();
                StartCoroutine(AttackCoroutine);
            }
        }
        else
            Follow();
    }
    protected virtual void GetDirectionToPlayer() {
        Direction = ( PlayerController.transform.position - transform.position ).normalized;
    }
    protected virtual void Retreat() {
        if ( DistanceFromPlayer < RetreatRange ) {
            Rb.MovePosition(transform.position + 2f * -MovementSpeed * Time.deltaTime * Direction);
        }
    }
    protected virtual void Follow() {
        Rb.MovePosition(transform.position + MovementSpeed * Time.deltaTime * Direction);
    }

    protected override void OnDrawGizmosSelected() {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, RetreatRange);
    }


    protected virtual IEnumerator Attack()
    {
        IsAttacking = true;

        GameObject bulletPrefab = Instantiate(BulletPrefab, transform.position, transform.rotation);
        bulletPrefab.GetComponent<Rigidbody2D>().AddForce(Direction * _bulletForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(_cooldown);
        IsAttacking = false;
    }
 }
