using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnersManager : Manager {
    [Header("Melee Enemy")]
    public GameObject[] MeleeEnemyPrefabs;
    [Range(1, 3)] public int CurrentMeleeEnemyLevel = 1;

    [Header("Shooter Enemy")]
    public GameObject[] ShooterEnemyPrefabs;
    [Range(1, 3)] public int CurrentShooterEnemyLevel = 1;

    [Header("Mover Enemy")]
    public GameObject[] MoverEnemyPrefabs;
    [Range(1, 3)] public int CurrentMoverEnemyLevel = 1;

    [Header("Tank Enemy")]
    public GameObject[] TankEnemyPrefabs;
    [Range(1, 3)] public int CurrentTankEnemyLevel = 1;

    [Header("Buffer Enemy")]
    public GameObject[] BufferEnemyPrefabs;
    [Range(1, 3)] public int CurrentBufferEnemyLevel = 1;

    [SerializeField] private GameObject[] _enemySpawners;
    [SerializeField] private int _currentWave = 0;
    private void Start() {
        _enemySpawners[0].SetActive(true);
    }

    private void Update() {
        EnemySpawner currentEnemySpawner = _enemySpawners[_currentWave].GetComponent<EnemySpawner>();
        if ( currentEnemySpawner.IsCleared ) {
            _enemySpawners[_currentWave].SetActive(false);
            _currentWave++;
            _enemySpawners[_currentWave].SetActive(true);
            currentEnemySpawner.IsCleared = false;
        }


    }
}
