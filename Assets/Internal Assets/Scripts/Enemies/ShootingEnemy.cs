using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingEnemy : Enemy {
    [SerializeField]
    protected GameObject BulletPrefab;
    [SerializeField]
    protected float _bulletForce = 20f;
    protected Rigidbody2D BulletPrefabRB;

    protected bool IsAttacking;

    protected override void Attack()
    {
        GameObject bulletPrefab = Instantiate(BulletPrefab, transform.position, transform.rotation);
        bulletPrefab.GetComponent<Rigidbody2D>().AddForce(Direction * _bulletForce, ForceMode2D.Impulse);
        AudioManager.PlaySFX(AudioManager.EnemyRangedAttack);
        lastAttackTime = Time.time;
    }
 }
