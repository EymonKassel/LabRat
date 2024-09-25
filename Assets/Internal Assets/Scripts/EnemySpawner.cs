using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField]
    private EnemySpawnerEntity[] _entities;

    private void Start() {
        foreach (EnemySpawnerEntity enemy in _entities) {
            Instantiate(enemy.Enemy.gameObject, enemy.Position, Quaternion.identity);
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        for ( int i = 0; i < _entities.Length; i++ ) {
            Gizmos.DrawWireSphere(_entities[i].Position, 1);
        }
    }
}

[Serializable]
public class EnemySpawnerEntity {
    public GameObject Enemy;
    public Vector2 Position;
}