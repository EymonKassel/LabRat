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
    public AudioClip BackgroundMusic;
    [SerializeField]
    public AudioClip PlayerShoot;

    private void Start() {
        _musicSourse.clip = BackgroundMusic;
        _musicSourse.Play();
    }

    public void PlaySFX(AudioClip clip) {
        _SFXSourse.PlayOneShot(clip);
    }
}
