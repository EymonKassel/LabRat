using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {
    [SerializeField] private EnemyBatch[] _batches;
    [SerializeField] private WaveManager _waveManager;

    public bool IsCleared = false;
    [SerializeField] private int _enemiesLeft;
    private void Awake() {
        _waveManager = FindObjectOfType<WaveManager>();
    }
    private void OnEnable() 
    {
        foreach (EnemyBatch batch in _batches)
        {
            _enemiesLeft += batch.enemyAmount;
            StartCoroutine(SpawnEnemies(batch));
        }
    }

    private void Update() 
    {
        CheckEnemiesLeft();
    }


    private void CheckEnemiesLeft() {
        if ( _enemiesLeft <= 0 ) {
            IsCleared = true;
        }
    }

    public void EnemyDied()
    {
        _enemiesLeft--;
    }

    private IEnumerator SpawnEnemies(EnemyBatch batch)
    {
        GameObject enemyToSpawn = null;
        switch (batch.EnemyType)
        {
            case EnemyType.Meele:
                enemyToSpawn = _waveManager.MeleeEnemyPrefabs[_waveManager.MeleeEnemyLevel];
                break;
            case EnemyType.Shooter:
                enemyToSpawn = _waveManager.ShooterEnemyPrefabs[_waveManager.ShooterEnemyLevel];
                break;
            case EnemyType.Running:
                enemyToSpawn = _waveManager.RunningEnemyPrefabs[_waveManager.RunningEnemyLevel];
                break;
            case EnemyType.Tank:
                enemyToSpawn = _waveManager.TankEnemyPrefabs[_waveManager.TankEnemyLevel];
                break;
            case EnemyType.Support:
                enemyToSpawn = _waveManager.SupportEnemyPrefabs[_waveManager.SupportEnemyLevel];
                break;
            default:
                Debug.LogError("Enemy type is not set");
                break;
        }

        yield return new WaitForSeconds(batch.spawnTime);

        for (int i = 0; i < batch.enemyAmount; i++)
        {
            Instantiate(enemyToSpawn, batch.RadiousCenter + UnityEngine.Random.insideUnitCircle * batch.Radious, Quaternion.identity);
        }
    }


    private void OnDrawGizmosSelected() {
        DrawGizmosEnemyPositions();
    }

    private void DrawGizmosEnemyPositions() {
        Gizmos.color = Color.green;
        for ( int i = 0; i < _batches.Length; i++ ) {
            Gizmos.DrawWireSphere(_batches[i].RadiousCenter, _batches[i].Radious);
        }
    }
}

[Serializable]
public class EnemyBatch {
    public EnemyType EnemyType = EnemyType.Meele;
    public Vector2 RadiousCenter;
    public float Radious = 1;
    public int enemyAmount = 1;
    public float spawnTime = 0;
}