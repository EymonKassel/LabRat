using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunEnemy : ShootingEnemy {
    [SerializeField]
    private float _sideBulletsAngle = 30f;

    protected override void Attack() {
        Animator.SetTrigger("IsAttacking");

        ChooseBulletDirection(Direction);
        ChooseBulletDirection(Quaternion.Euler(0, 0, _sideBulletsAngle) * (PlayerPosition.position - transform.position).normalized);
        ChooseBulletDirection(Quaternion.Euler(0, 0, -_sideBulletsAngle) * (PlayerPosition.position - transform.position).normalized);

    }

    void ChooseBulletDirection(Vector3 direction)
    {
        GameObject bulletPrefab = Instantiate(BulletPrefab, _firePoint.position, transform.rotation);
        bulletPrefab.GetComponent<Rigidbody2D>().AddForce(direction * _bulletForce, ForceMode2D.Impulse);
        lastAttackTime = Time.time;
    }
}
