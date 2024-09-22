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
    private AudioClip _temp;

    private void Start() {

    }

    public void PlaySFX(AudioClip clip) {
        _SFXSourse.PlayOneShot(clip);
    }
}
