using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy {

    protected override void Attack() 
    {
        PlayerController.TakeDamage();
        //AudioManager.PlaySFX(AudioManager.EnemyMeleeAttack);
    }
}