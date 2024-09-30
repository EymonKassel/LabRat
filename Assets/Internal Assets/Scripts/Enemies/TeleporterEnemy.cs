using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterEnemy : ShootingEnemy
{
    private GameManager _gameManager;

    protected override void OnEnable()
    {
        base.OnEnable();
        _gameManager = FindAnyObjectByType<GameManager>();
    }

    protected override void Movement()
    {
        if (Time.time - lastAttackTime > _cooldown)
        {
            gameObject.transform.position = _gameManager._teleportPoints[Random.Range(0, _gameManager._teleportPoints.Length)].position;
            GetDirectionToPlayer();
            Attack();
        }
    }
}
