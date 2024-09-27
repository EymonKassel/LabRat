using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ShotgunEnemy : ShootingEnemy {
    [SerializeField]
    private float _sideBulletsAngle = 30f;

    protected override IEnumerator Attack() {
        IsAttacking = true;
        //_animator.SetBool("IsAttacking", true);

        ChooseBulletDirection(Direction);
        ChooseBulletDirection(Quaternion.Euler(0, 0, _sideBulletsAngle) * (PlayerPosition.position - transform.position).normalized);
        ChooseBulletDirection(Quaternion.Euler(0, 0, -_sideBulletsAngle) * (PlayerPosition.position - transform.position).normalized);

        yield return new WaitForSeconds(_cooldown);
        IsAttacking = false;
        //_animator.SetBool("IsAttacking", false);
    }

    void ChooseBulletDirection(Vector3 direction)
    {
        GameObject bulletPrefab = Instantiate(BulletPrefab, transform.position, transform.rotation);
        bulletPrefab.GetComponent<Rigidbody2D>().AddForce(direction * _bulletForce, ForceMode2D.Impulse);
    }
}
