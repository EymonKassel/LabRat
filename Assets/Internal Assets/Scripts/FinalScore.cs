using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScore : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _waves;
    [SerializeField] private TextMeshProUGUI _shots;
    [SerializeField] private TextMeshProUGUI _dashes;


    private ScoreCounter _scoreCounter;
    private void Awake() {
        _scoreCounter = FindObjectOfType<ScoreCounter>();
    }

    private void Start() {
        ShowFinalScore();
    }
    private void ShowFinalScore() {
        _waves.text = "Waves completed: " + _scoreCounter.WaveCounter;
        _shots.text = "Shots taken: " + _scoreCounter.ShotCounter;
        _dashes.text = "Dashes done: " + _scoreCounter.DashCounter;
    }
}
