using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy {

    protected override void Attack()
    {
        Animator.SetTrigger("IsAttacking");
        PlayerController.TakeDamage();
        AudioManager.PlaySFX(AudioManager.EnemyMeleeAttack);
        lastAttackTime = Time.time;
    }
}