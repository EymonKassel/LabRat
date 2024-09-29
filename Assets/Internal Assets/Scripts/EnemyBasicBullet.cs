using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicBullet : MonoBehaviour {
    private float lifetime = 1f;
    private AudioManager _audioManager;
    private void Awake() {
        _audioManager = FindObjectOfType<AudioManager>();
    }
    private void Start() {
        Destroy(gameObject, lifetime);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if ( collision.gameObject.GetComponent<PlayerController>() != null ) {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
            Destroy(gameObject);
        }
        _audioManager.PlaySFX(_audioManager.BasicShootDestroy);
        Destroy(gameObject);
    }

}
