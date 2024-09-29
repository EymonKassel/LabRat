using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Manager {
    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSourse;
    [SerializeField] private AudioSource _SFXSourse;

    [Header("Audio Clips")]
    [Header("Music")]
    public AudioClip BackgroundMusic;
    
    [Header("Player")]
    public AudioClip PlayerBasicShoot;
    public AudioClip BasicShootDestroy;
    public AudioClip PlayerDeath;
    public AudioClip PlayerDash;
    public AudioClip PlayerTakingDamage;

    [Header("Enemies")]
    public AudioClip EnemyMeleeAttack;
    public AudioClip EnemyRangedAttack;
    public AudioClip EnemyDeath;
    public AudioClip EnemyBirth;
    public AudioClip EnemyBirthFromFloor;
    public AudioClip EnemyDash;

    [Header("UI")]
    public AudioClip PressButton;

    private void Start() {
        _musicSourse.clip = BackgroundMusic;
        _musicSourse.Play();
    }

    public void PlaySFX(AudioClip clip) {
        _SFXSourse.PlayOneShot(clip);
    }
}
