using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Manager {
    [SerializeField] private Image _waveBar;
    private WaveManager _waveManager;
    public int _waveCapacity;
    public Transform[] _teleportPoints;
    private float _scaleFactor;

    private PlayerController _playerController;
    private Gun _gun;

    [SerializeField] private int _maxEnemyLevel = 2;

    [SerializeField] private GameObject _levelUpPanel;

    [SerializeField] private GameObject[] _healthImages;

    


    private void Awake() {
        
        
    }
    private void Start() {
        _waveManager = FindObjectOfType<WaveManager>();
        _playerController = FindObjectOfType<PlayerController>();
        _gun = FindObjectOfType<Gun>();
        _waveCapacity = _waveManager._waves[_waveManager.CurrentWave].GetComponent<Wave>().MaxEnemyAmount;
        UpdateWaveBar();
        UpdateHealthBar();
    }
    private void Update() {
        UpdateWaveBar();
        UpdateHealthBar();

    }

    public void LoadNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SwitcherForSound(GameObject gameObject) {
        if ( gameObject.activeInHierarchy ) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
    }
    private void UpdateHealthBar() {
        for ( int i = 0; i < _playerController.TakenDamage; i++ ) {
            _healthImages[i].SetActive(false);
        }
    }
    private void UpdateWaveBar() {
        float _scaleFactor = (float)_waveManager._waves[_waveManager.CurrentWave].GetComponent<Wave>()._enemiesLeft / _waveCapacity;
        _scaleFactor = Mathf.Clamp(_scaleFactor, 0f, 1f);
        _waveBar.transform.localScale = new Vector3(_scaleFactor, 1, 1);
    }
    
    public void LevelUpEnemies(GetEnum g) {
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
        _waveManager.PickedUpgrade = true;
        _playerController.enabled = true;
        _gun.enabled = true;
        _levelUpPanel.SetActive(false);
    }
    public void ShowLevelUpPanel() {
        _levelUpPanel.SetActive(true);
        _playerController.enabled = false;
        _playerController._rb.velocity = Vector2.zero;
        _gun.enabled = false;
    }
}
