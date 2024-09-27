using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    private float lifetime = 1f;
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
}
