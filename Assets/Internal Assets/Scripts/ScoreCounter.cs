using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour {
    public int WaveCounter;
    public int ShotCounter;
    public int DashCounter;

    private WaveManager _waveManager;
    private PlayerController _playerController;
    private Gun _gun;
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    private void Start() {
        _waveManager = FindObjectOfType<WaveManager>();
        _playerController = FindObjectOfType<PlayerController>();
        _gun = FindObjectOfType<Gun>();
    }
    private void Update() {
        WaveCounter = _waveManager.WaveCounter;
        ShotCounter = _gun.BulletCounter;
        DashCounter = _playerController.DashCounter;
    }
}
