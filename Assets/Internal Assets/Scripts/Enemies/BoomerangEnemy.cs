using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoomerangEnemy : ShootingEnemy {
    [SerializeField]
    private float _cooldown = 1f;
    protected override void Update() {
        base.Update();

        if ( DistanceFromPlayer < RetreatRange ) {
            Retreat();
        } else if ( DistanceFromPlayer < AttackRange ) {
            if ( !IsAttacking ) {

                AttackCoroutine = Attack();
                StartCoroutine(AttackCoroutine);
            }
        } else {
            Follow();
        }
    }

    protected virtual IEnumerator Attack() {
        IsAttacking = true;
        //_animator.SetBool("IsAttacking", true);

        GameObject bulletPrefab = Instantiate(BulletPrefab, transform.position, transform.rotation);
        Rigidbody2D bulletPrefabRB = bulletPrefab.GetComponent<Rigidbody2D>();
        
        // To do

        yield return new WaitForSeconds(_cooldown);
        IsAttacking = false;
        //_animator.SetBool("IsAttacking", false);
    }
}
