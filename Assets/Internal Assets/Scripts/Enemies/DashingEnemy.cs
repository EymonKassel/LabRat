using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingEnemy : Enemy 
{
    private bool _lockedOnPlayer = false;
    protected override void Movement()
    {
        if (Time.time - lastAttackTime > _cooldown)
        {
            if (!_lockedOnPlayer)
            {
                GetDirectionToPlayer();
                _lockedOnPlayer = true;
            }
            Rb.MovePosition(transform.position + MovementSpeed * Time.fixedDeltaTime * Direction);
        }
            
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (_lockedOnPlayer && !collision.gameObject.GetComponent<BasicBullet>())
        {
            _lockedOnPlayer = false;
            lastAttackTime = Time.time;
        }
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }
}
