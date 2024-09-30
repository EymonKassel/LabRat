using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int WaveCounter;
    public GameObject[] _waves;
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

    public int CurrentWave = 0;

    private GameManager _gameManager;

    private void Awake() {
        _gameManager = FindObjectOfType<GameManager>();
    }
    private void OnEnable() {
        Debug.Log(_waves[0]);
        _waves[0].SetActive(true);
    }

    private void Update() {
        if (_waves[CurrentWave].GetComponent<Wave>().IsCleared ) {
            _waves[CurrentWave].SetActive(false);
            CurrentWave = (CurrentWave + 1) % _waves.Length;
            WaveCounter++;
            _waves[CurrentWave].SetActive(true);
            _waves[CurrentWave].GetComponent<Wave>().IsCleared = false;
            _gameManager.ShowLevelUpPanel();
        }
    }
}
