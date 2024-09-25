using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    private float lifetime = 1f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Some");
        Destroy(gameObject);
    }
}
