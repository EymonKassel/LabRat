using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingEnemy : Enemy {
    [SerializeField]
    protected GameObject BulletPrefab;
    [SerializeField]
    protected float _bulletForce = 20f;
    [SerializeField]
    protected Transform _firePoint;
    protected Rigidbody2D BulletPrefabRB;

    protected override void Attack()
    {
        Animator.SetTrigger("IsAttacking");
        GameObject bulletPrefab = Instantiate(BulletPrefab, _firePoint.position, _firePoint.rotation);
        bulletPrefab.GetComponent<Rigidbody2D>().AddForce(Direction * _bulletForce, ForceMode2D.Impulse);
        AudioManager.PlaySFX(AudioManager.EnemyRangedAttack);
        lastAttackTime = Time.time;
    }
 }
