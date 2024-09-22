using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Manager {
    [Header("Audio Sources")]
    [SerializeField]
    private AudioSource _musicSourse;
    [SerializeField]
    private AudioSource _SFXSourse;

    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip _backgroundMusic;
    [SerializeField]
    private AudioClip _playerShoot;

    private void Start() {
        _musicSourse.clip = _backgroundMusic;
        _musicSourse.Play();
    }

    public void PlaySFX(AudioClip clip) {
        _SFXSourse.PlayOneShot(clip);
    }
}
