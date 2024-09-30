using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : Enemy
{
    protected override void Attack()
    {
        Animator.SetTrigger("IsAttacking");
        PlayerController.TakeDamage();
        AudioManager.PlaySFX(AudioManager.EnemyRangedAttack);
        CurrentHealth = 0;
    }
}
