using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _waves;
    [Header("Melee Enemy")]
    public GameObject[] MeleeEnemyPrefabs;
    [Range(0, 2)] public int MeleeEnemyLevel = 1;

    [Header("Shooter Enemy")]
    public GameObject[] ShooterEnemyPrefabs;
    [Range(0, 2)] public int ShooterEnemyLevel = 1;

    [Header("Running Enemy")]
    public GameObject[] RunningEnemyPrefabs;
    [Range(0, 2)] public int RunningEnemyLevel = 1;

    [Header("Tank Enemy")]
    public GameObject[] TankEnemyPrefabs;
    [Range(0, 2)] public int TankEnemyLevel = 1;

    [Header("Support Enemy")]
    public GameObject[] SupportEnemyPrefabs;
    [Range(0, 2)] public int SupportEnemyLevel = 1;

    [SerializeField] private int _currentWave = 0;

    private void OnEnable() {
        Debug.Log(_waves[0]);
        _waves[0].SetActive(true);
    }

    private void Update() {
        if (_waves[_currentWave].GetComponent<Wave>().IsCleared ) {
            _waves[_currentWave].SetActive(false);
            _currentWave = (_currentWave + 1) % _waves.Length;
            _waves[_currentWave].SetActive(true);
            _waves[_currentWave].GetComponent<Wave>().IsCleared = false;
        }
    }
}
