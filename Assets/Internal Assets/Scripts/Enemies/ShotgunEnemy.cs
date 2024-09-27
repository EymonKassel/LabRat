using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ShotgunEnemy : ShootingEnemy {
    [SerializeField]
    private float _cooldown = 1f;
    [SerializeField]
    private float _middleBulletForce = 20f;
    [SerializeField]
    private float _sideBulletsForce = 20f;
    [SerializeField]
    private float _sideBulletsAngle = 30f;
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

        GameObject bulletPrefabMiddle = Instantiate(BulletPrefab, transform.position, transform.rotation);
        Rigidbody2D bulletPrefabMiddleRB = bulletPrefabMiddle.GetComponent<Rigidbody2D>();
        bulletPrefabMiddleRB.AddForce(Direction * _middleBulletForce, ForceMode2D.Impulse);

        var direction = Quaternion.Euler(0, 0, _sideBulletsAngle) * ( PlayerPosition.position - transform.position ).normalized;
        GameObject bulletPrefabLeft = Instantiate(BulletPrefab, transform.position, transform.rotation);
        Rigidbody2D bulletPrefabLeftRB = bulletPrefabLeft.GetComponent<Rigidbody2D>();
        bulletPrefabLeftRB.AddForce(direction * _sideBulletsForce, ForceMode2D.Impulse);

        direction = Quaternion.Euler(0, 0, -_sideBulletsAngle) * ( PlayerPosition.position - transform.position ).normalized;
        GameObject bulletPrefabRight = Instantiate(BulletPrefab, transform.position, transform.rotation);
        Rigidbody2D bulletPrefabRightRB = bulletPrefabRight.GetComponent<Rigidbody2D>();
        bulletPrefabRightRB.AddForce(direction * _sideBulletsForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(_cooldown);
        IsAttacking = false;
        //_animator.SetBool("IsAttacking", false);
    }
}
