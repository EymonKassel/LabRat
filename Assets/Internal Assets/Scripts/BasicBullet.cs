using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    private AudioManager _audioManager;
    private float _lifetime = 1f;
    private void Awake() {
        _audioManager = FindObjectOfType<AudioManager>();
    }
    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        _audioManager.PlaySFX(_audioManager.BasicShootDestroy);
        Destroy(gameObject);
    }
}
