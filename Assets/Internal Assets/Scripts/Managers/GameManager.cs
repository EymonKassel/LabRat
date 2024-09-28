using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Manager {
    [SerializeField]
    private GameObject _uiSettingsPanel;
    [SerializeField]
    private Image _waveBar;
    [SerializeField]
    private GameObject[] _enemiesArray;
    private int _maxEnemiesInWave;
    [SerializeField]
    private int _currentWave = 0;
    [SerializeField]
    private EnemySpawner[] _enemySpawners;
    private void Start() {
        _maxEnemiesInWave = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UpdateWaveBar();
    }
    private void FixedUpdate() {
        _enemiesArray = GameObject.FindGameObjectsWithTag("Enemy");
        //if ( _enemiesArray.Length <= 0 ) {
        //    _enemySpawners[_currentWave++].enabled = true;
        //}
        
        UpdateWaveBar();
    }

    private void UpdateWaveBar() {
        float scaleFactor = (float)_enemiesArray.Length / _maxEnemiesInWave;
        scaleFactor = Mathf.Clamp(scaleFactor, 0f, 1f);
        _waveBar.transform.localScale = new Vector3(scaleFactor, 1, 1);
    }
    
}
