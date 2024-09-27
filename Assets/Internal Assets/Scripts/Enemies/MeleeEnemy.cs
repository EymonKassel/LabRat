using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class MeleeEnemy : Enemy {
    private bool _isAttacking;
    private IEnumerator _attackCoroutine;

    protected override void Update() {
        base.Update();
        if ( Rb.velocity.magnitude <= 0.1f ) {
            Animator.SetBool("IsMoving", false);
        }

        if ( CurrentHealth <= 0 ) {
            // Improve later
            Animator.SetBool("IsDead", true);
        }

        if ( DistanceFromPlayer < AttackRange ) {
            if ( !_isAttacking ) {
                _attackCoroutine = Attack();
                StartCoroutine(_attackCoroutine);
            }

        } else {
            Follow();
        }
    }
    private IEnumerator Attack() {
        _isAttacking = true;
        Animator.SetBool("IsAttacking", true);
        PlayerController.CurrentHealth--;
        yield return new WaitForSeconds(1);
        _isAttacking = false;
        Animator.SetBool("IsAttacking", false);
    }
    private void Follow() {
        Vector3 direction = ( PlayerController.transform.position - transform.position ).normalized;
        Rb.MovePosition(transform.position + direction * MovementSpeed * Time.deltaTime);
    }
}