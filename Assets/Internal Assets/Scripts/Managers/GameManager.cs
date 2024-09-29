using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Manager {
    [SerializeField] private Image _waveBar;
    private WaveManager _waveManager;
    private int _enemiesLeft;
    private float _scaleFactor;

    private PlayerController _playerController;
    private Gun _gun;

    [SerializeField] private int _maxEnemyLevel = 2;

    [SerializeField] private GameObject _levelUpPanel;

    private void Awake() {
        _waveManager = FindObjectOfType<WaveManager>();
        _playerController = FindObjectOfType<PlayerController>();
        _gun = FindObjectOfType<Gun>();
    }
    private void Start() {
        UpdateWaveBar();
    }
    private void Update() {
        UpdateWaveBar();

    }
    private void UpdateWaveBar() {
        float _scaleFactor = (float)_waveManager._waves[_waveManager.CurrentWave].GetComponent<Wave>()._enemiesLeft 
            / _waveManager._waves[_waveManager.CurrentWave].GetComponent<Wave>().MaxEnemyAmount;
        _scaleFactor = Mathf.Clamp(_scaleFactor, 0f, 1f);
        _waveBar.transform.localScale = new Vector3(_scaleFactor, 1, 1);
    }
    
    public void LevelUpEnemies(GetEnum g) {
        _playerController.enabled = false;
        _gun.enabled = false;
        switch ( g.State ) {
            case EnemyType.Meele:
                if (_waveManager.MeleeEnemyLevel < _maxEnemyLevel ) {
                    _waveManager.MeleeEnemyLevel++;
                }
                break;
            case EnemyType.Shooter:
                if ( _waveManager.ShooterEnemyLevel < _maxEnemyLevel ) {
                    _waveManager.ShooterEnemyLevel++;
                }
                break;
            case EnemyType.Running:
                if ( _waveManager.RunningEnemyLevel < _maxEnemyLevel ) {
                    _waveManager.RunningEnemyLevel++;
                }
                break;
            case EnemyType.Tank:
                if ( _waveManager.TankEnemyLevel < _maxEnemyLevel ) {
                    _waveManager.TankEnemyLevel++;
                }
                break;
            case EnemyType.Support:
                if ( _waveManager.SupportEnemyLevel < _maxEnemyLevel ) {
                    _waveManager.SupportEnemyLevel++;
                }
                break;
            default:
                Debug.Log("Invalid enemy type");
                break;
        }
        _playerController.enabled = true;
        _gun.enabled = true;
        _levelUpPanel.SetActive(false);
    }
    public void ShowLevelUpPanel() {
        _levelUpPanel.SetActive(true);
    }
}
