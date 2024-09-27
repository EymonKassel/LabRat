using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy {
    [SerializeField]
    protected float RetreatRange = 2f;
    [SerializeField]
    protected GameObject BulletPrefab;
    protected Rigidbody2D BulletPrefabRB;

    protected bool IsAttacking;
    protected IEnumerator AttackCoroutine;
    protected Vector3 Direction;

    protected override void Update() {
        base.Update();

        GetDirectionToPlayer();
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
}
