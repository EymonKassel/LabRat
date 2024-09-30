using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunEnemy : ShootingEnemy {
    [SerializeField]
    private float _sideBulletsAngle = 30f;

    protected override void Attack() {
        //_animator.SetBool("IsAttacking", true);

        ChooseBulletDirection(Direction);
        ChooseBulletDirection(Quaternion.Euler(0, 0, _sideBulletsAngle) * (PlayerPosition.position - transform.position).normalized);
        ChooseBulletDirection(Quaternion.Euler(0, 0, -_sideBulletsAngle) * (PlayerPosition.position - transform.position).normalized);

        //_animator.SetBool("IsAttacking", false);
    }

    void ChooseBulletDirection(Vector3 direction)
    {
        GameObject bulletPrefab = Instantiate(BulletPrefab, transform.position, transform.rotation);
        bulletPrefab.GetComponent<Rigidbody2D>().AddForce(direction * _bulletForce, ForceMode2D.Impulse);
        lastAttackTime = Time.time;
    }
}
