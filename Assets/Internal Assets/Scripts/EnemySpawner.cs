using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private EnemySpawnerEntity[] _entities;
    [SerializeField] private EnemySpawnersManager _enemySpawnersManager;

    private EnemyType _enemyType;
    private GameObject _meleeEnemyPrefab;
    private GameObject _shooterEnemyPrefab;
    private GameObject _moverEnemyPrefab;
    private GameObject _tankEnemyPrefab;
    private GameObject _bufferEnemyPrefab;

    public bool IsCleared;
    [SerializeField] private int _enemiesLeft;
    private void Awake() {
        _enemySpawnersManager = FindObjectOfType<EnemySpawnersManager>();
    }
    private void Start() {
        SetEnemyPrefabs();
        SpawnEnemies();
    }

    private void SetEnemyPrefabs() {
        _meleeEnemyPrefab = _enemySpawnersManager.MeleeEnemyPrefabs[_enemySpawnersManager.CurrentMeleeEnemyLevel];
        _shooterEnemyPrefab = _enemySpawnersManager.ShooterEnemyPrefabs[_enemySpawnersManager.CurrentShooterEnemyLevel];
        _moverEnemyPrefab = _enemySpawnersManager.MoverEnemyPrefabs[_enemySpawnersManager.CurrentMoverEnemyLevel];
        _tankEnemyPrefab = _enemySpawnersManager.MoverEnemyPrefabs[_enemySpawnersManager.CurrentMoverEnemyLevel];
        _bufferEnemyPrefab = _enemySpawnersManager.MoverEnemyPrefabs[_enemySpawnersManager.CurrentBufferEnemyLevel];

    }
    private void Update() {
        CheckEnemiesLeft();
    }
    private void CheckEnemiesLeft() {
        _enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if ( _enemiesLeft <= 0 ) {
            IsCleared = true;
        }
    }
    private void SpawnEnemies() {
        foreach ( EnemySpawnerEntity enemy in _entities ) {
            //todo
            Instantiate(enemy.Enemy, enemy.Position, Quaternion.identity);
        }
        _enemiesLeft = _entities.Length;
    }
    private void OnDrawGizmosSelected() {
        DrawGizmosEnemyPositions();
    }
    private void DrawGizmosEnemyPositions() {
        Gizmos.color = Color.green;
        for ( int i = 0; i < _entities.Length; i++ ) {
            Gizmos.DrawWireSphere(_entities[i].Position, 1);
        }
    }
}

[Serializable]
public class EnemySpawnerEntity {
    public GameObject Enemy;
    public EnemyType EnemyType = EnemyType.Meele;
    public Vector2 Position;
}