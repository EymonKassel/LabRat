using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RunningEnemy : Enemy
{
    protected override void OnEnable()
    {
        base.OnEnable();
        float random = Random.Range(0f, 260f);
        Direction = new Vector3(Mathf.Cos(random), Mathf.Sin(random), 1);
    }

    protected override void Movement()
    {
        Rb.MovePosition(transform.position + MovementSpeed * Time.fixedDeltaTime * Direction);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (!collision.gameObject.GetComponent<BasicBullet>())
        {
            Direction = Vector2.Reflect(transform.position, collision.transform.position).normalized;
        }
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }
}
