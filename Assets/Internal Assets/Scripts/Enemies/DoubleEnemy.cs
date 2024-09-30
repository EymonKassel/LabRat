using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleEnemy : Enemy
{
    [SerializeField] GameObject _anotherBody;

    protected override void Movement()
    {
        GetDistanceFromPlayer();
        GetDirectionToPlayer();

        if (DistanceFromPlayer < RetreatRange)
            Rb.MovePosition(transform.position + 2f * -MovementSpeed * Time.fixedDeltaTime * Direction);
    }

    public override void Death()
    {
        Destroy(_anotherBody);
        base.Death();
    }
}
