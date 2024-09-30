using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy {

    protected override void Attack() 
    {
        Animator.SetBool("IsAttacking", true);
        PlayerController.TakeDamage();
        AudioManager.PlaySFX(AudioManager.EnemyMeleeAttack);
        Animator.SetBool("IsAttacking", false);
    }
}